using Godot;
using Godot.Collections;
using System;

public partial class GameObjectLoader : Node2D
{
    [Export]
	Node2D sceneData;
	SceneData _sceneData;
	SignalBus signalBus;
	PackedScene levelOne;
	PackedScene levelTwo;
	PackedScene levelThree;
	PackedScene sublevelOne;
	PackedScene player;
	PackedScene dmgOrb;
	int currentLevel;	

	private Godot.Collections.Array levelInstanceArray = new Godot.Collections.Array();
	CharacterBody2D bodyInstance;
	public override void _Ready()
	{ 
	   _sceneData = (SceneData)sceneData;
	   
	   levelOne = _sceneData.LevelOne;
	   levelTwo = _sceneData.LevelTwo;
	   levelThree = _sceneData.LevelThree;
	   player = _sceneData.Player;
	   sublevelOne = _sceneData.SubLevelOne;
		dmgOrb = _sceneData.DamageOrb;

		LoadLevel(levelOne);
		LoadLevel(sublevelOne);
		LoadLevel(levelTwo);
		LoadLevel(levelThree);

		//InstantiateLevels();

	   signalBus = GetNode<SignalBus>("/root/SignalBus");
	   signalBus.StartGame += () => AddChild((Node)levelInstanceArray[0]);
	   signalBus.StartGame += () => LoadCharacterBody(player, new Vector2(0,-10));

	   signalBus.SpecialAction += (Vector2 position) => {LoadCharacterBody(dmgOrb, position + new Vector2(0, 10)); signalBus.EmitSignal(SignalBus.SignalName.SFX, "Orb_Spawn");};

		signalBus.Warp += (int warpVal, Vector2 telePosition) => 
		{
			bodyInstance.Position = telePosition;
			FreeCharacterBody();
			
			
			GD.Print(bodyInstance.Position);
			switch (warpVal)
			{
			case 1:
			
				FreeLevel(1);
				CallDeferred("add_child", (Node)levelInstanceArray[0]);
				currentLevel = 0;
				if(bodyInstance.Position == telePosition)
				{
				CallDeferred("add_child", bodyInstance);
				}
				
	
			break;
			case -1:	
				FreeLevel(0);
				CallDeferred("add_child", (Node)levelInstanceArray[1]);
				currentLevel = 1;
				//LoadLevel(sublevelOne);
				if(bodyInstance.Position == telePosition)
				{
				CallDeferred("add_child", bodyInstance);
				}
			break;
			}

			
			
			
			
		};

		signalBus.LevelComplete += (int levelID) => 
		{
			FreeLevel(currentLevel);
			bodyInstance.Position = Vector2.Zero;
			switch (levelID)
			{
				case 1:
				CallDeferred("add_child", (Node)levelInstanceArray[0]);
				currentLevel = 0;
				break;
				case 2:
				CallDeferred("add_child", (Node)levelInstanceArray[2]);
				currentLevel = 2;
				break;
				case 3:
				CallDeferred("add_child", (Node)levelInstanceArray[3]);
				currentLevel = 3;
				break;
			}
		};

	   signalBus.GameOver += () => FreeLevel(currentLevel);
	   signalBus.GameOver += () => FreeCharacterBody();
	}

	private void LoadLevel(PackedScene level)
	{
		levelInstanceArray.Add((Node)level.Instantiate());
	}

	private void LoadCharacterBody(PackedScene body, Vector2 position)
	{
		bodyInstance = (CharacterBody2D)body.Instantiate();
		AddChild(bodyInstance);
		bodyInstance.Position = position;
	}

	private void FreeLevel(int index)
	{
		CallDeferred("remove_child", levelInstanceArray[index]);
	}

	private void FreeCharacterBody()
	{
		CallDeferred("remove_child", bodyInstance);
	}
}