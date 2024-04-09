using Godot;
using System;

public partial class SceneData : Node2D
{
	SignalBus signalBus;
	public PackedScene MainMenu {get; private set;} = GD.Load<PackedScene>("res://scenes/main_menu.tscn");
	public PackedScene LevelOne {get; private set; } = GD.Load<PackedScene>("res://scenes/level_1.tscn");
    public PackedScene LevelTwo {get; private set;} = GD.Load<PackedScene>("res://scenes/level_2.tscn");
	public PackedScene LevelThree {get; private set;} = GD.Load<PackedScene>("res://scenes/level_3.tscn");
	public PackedScene Player{get; private set;} = GD.Load<PackedScene>("res://scenes/player.tscn");

	
	
	Control mainMenuInstance;
	public override void _Ready()
	{
			
		

		//gameWorld = GetNode<LevelLoader>("Game_World");

	    //mainMenuInstance = (Control)MainMenu.Instantiate();
		//AddChild(mainMenuInstance);
	}

	public void OnStartGame()
	{
		//RemoveChild(mainMenuInstance);

		//var playerInstance = (CharacterBody2D)Player.Instantiate();
		//AddChild(playerInstance);

		//gameWorld.GetLevel(LevelOne);
	}


}
