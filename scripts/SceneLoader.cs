using Godot;
using System;

public partial class SceneLoader : Node2D
{
	SignalBus signalBus;
	LevelLoader gameWorld;
	PackedScene mainMenu = GD.Load<PackedScene>("res://scenes/main_menu.tscn");
	PackedScene level1 = GD.Load<PackedScene>("res://scenes/level_1.tscn");
	PackedScene level2 = GD.Load<PackedScene>("res://scenes/level_2.tscn");
	PackedScene level3 = GD.Load<PackedScene>("res://scenes/level_3.tscn");
	PackedScene player = GD.Load<PackedScene>("res://scenes/player.tscn");
	Control mainMenuInstance;
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");
		signalBus.StartGame += OnStartGame;

		gameWorld = GetNode<LevelLoader>("Game_World");

	    mainMenuInstance = (Control)mainMenu.Instantiate();
		AddChild(mainMenuInstance);
	}

	public void OnStartGame()
	{
		RemoveChild(mainMenuInstance);

		var playerInstance = (CharacterBody2D)player.Instantiate();
		AddChild(playerInstance);

		gameWorld.GetLevel(level1);
	}


}
