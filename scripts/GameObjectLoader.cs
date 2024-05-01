using Godot;
using System;

public partial class GameObjectLoader : Node2D
{
    [Export]
	Node2D sceneData;
	SceneData _sceneData;
	SignalBus signalBus;
	PackedScene levelOne;
	PackedScene player;
	Node levelInstance; 
	CharacterBody2D bodyInstance;
	public override void _Ready()
	{ 
	   _sceneData = (SceneData)sceneData;
	   
	   levelOne = _sceneData.LevelOne;
	   player = _sceneData.Player;

	   signalBus = GetNode<SignalBus>("/root/SignalBus");
	   signalBus.StartGame += () => LoadLevel(levelOne);
	   signalBus.StartGame += () => LoadCharacterBody(player);
	   signalBus.GameOver += () => FreeLevel();
	}

	private void LoadLevel(PackedScene level)
	{
		levelInstance = (Node)level.Instantiate();
		AddChild(levelInstance);
	}

	private void LoadCharacterBody(PackedScene body)
	{
		bodyInstance = (CharacterBody2D)body.Instantiate();
		AddChild(bodyInstance);
	}

	private void FreeLevel()
	{
		RemoveChild(levelInstance);
	}

	private void FreeCharacterBody()
	{
		bodyInstance.QueueFree();
	}
}