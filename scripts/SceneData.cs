using Godot;
using System;

public partial class SceneData : Node2D
{
	SignalBus signalBus;
	public PackedScene MainMenu {get; private set;} = GD.Load<PackedScene>("res://scenes/main_menu.tscn");
	public PackedScene HUD {get; private set;} = GD.Load<PackedScene>("res://scenes/hud.tscn");
	public PackedScene GameOverScreen {get; private set;} = GD.Load<PackedScene>("res://scenes/game_over_screen.tscn");
	public PackedScene LevelOne {get; private set; } = GD.Load<PackedScene>("res://scenes/level_1.tscn");
	public PackedScene SubLevelOne {get; private set;} = GD.Load<PackedScene>("res://scenes/sub_level_1.tscn");
    public PackedScene LevelTwo {get; private set;} = GD.Load<PackedScene>("res://scenes/level_2.tscn");
	public PackedScene LevelThree {get; private set;} = GD.Load<PackedScene>("res://scenes/level_3.tscn");
	public PackedScene Player{get; private set;} = GD.Load<PackedScene>("res://scenes/player.tscn");
	public PackedScene DamageOrb{get; private set;} = GD.Load<PackedScene>("res://scenes/damage_orb.tscn");

	

}
