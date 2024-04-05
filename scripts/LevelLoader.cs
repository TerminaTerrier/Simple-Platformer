using Godot;
using System;

public partial class LevelLoader : Node2D
{
	Node levelInstance; 
	public override void _Ready()
	{
	}

	public void GetLevel(PackedScene level)
	{
		levelInstance = (Node)level.Instantiate();
		AddChild(levelInstance);
	}
}