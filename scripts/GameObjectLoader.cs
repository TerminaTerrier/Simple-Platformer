using Godot;
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
	Node levelInstance; 
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

	   signalBus = GetNode<SignalBus>("/root/SignalBus");
	   signalBus.StartGame += () => LoadLevel(levelOne);
	   signalBus.StartGame += () => LoadCharacterBody(player, new Vector2(0,-10));

	   signalBus.SpecialAction += (Vector2 position) => LoadCharacterBody(dmgOrb, position + new Vector2(0, 10));

		signalBus.Warp += (int warpVal, Vector2 telePosition) => 
		{
			FreeLevel();
			GD.Print(warpVal);
			switch (warpVal)
			{
			case 1:
			LoadLevel(levelOne);
			bodyInstance.Position = telePosition;
			break;
			case -1:	
			LoadLevel(sublevelOne);
			bodyInstance.Position = telePosition;
			break;
			}
		};

		signalBus.LevelComplete += (int levelID) => 
		{
			FreeLevel();
			bodyInstance.Position = Vector2.Zero;
			switch (levelID)
			{
				case 1:
				LoadLevel(levelOne);
				break;
				case 2:
				LoadLevel(levelTwo);
				break;
				case 3:
				LoadLevel(levelThree);
				break;
			}
		};

	   signalBus.GameOver += () => FreeLevel();
	   signalBus.GameOver += () => FreeCharacterBody();
	}

	private void LoadLevel(PackedScene level)
	{
		levelInstance = (Node)level.Instantiate();
		AddChild(levelInstance);
	}

	private void LoadCharacterBody(PackedScene body, Vector2 position)
	{
		bodyInstance = (CharacterBody2D)body.Instantiate();
		AddChild(bodyInstance);
		bodyInstance.Position = position;
	}

	private void FreeLevel()
	{
		CallDeferred("remove_child", levelInstance);
	}

	private void FreeCharacterBody()
	{
		CallDeferred("remove_child", bodyInstance);
	}
}