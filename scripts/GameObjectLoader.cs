using Godot;
using System;

public partial class GameObjectLoader : Node2D
{
    [Export]
	Node2D sceneData;
	SceneData _sceneData;
	SignalBus signalBus;
	PackedScene levelOne;
	PackedScene sublevelOne;
	PackedScene player;
	PackedScene dmgOrb;
	Node levelInstance; 
	CharacterBody2D bodyInstance;
	public override void _Ready()
	{ 
	   _sceneData = (SceneData)sceneData;
	   
	   levelOne = _sceneData.LevelOne;
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