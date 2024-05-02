using Godot;
using System;

public partial class Stats : Resource
{
	[Export]
	public int Damage { get; set; } = 1;
	[Export]
	public int MaxHealth { get; set; } = 3;
	[Export]
	public int StartingHealth { get; set; } = 1;
}
