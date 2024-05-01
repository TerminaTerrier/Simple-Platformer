using Godot;
using System;

public partial class Stats : Resource
{
	[Export]
	public int Damage { get; set; } = 1;
	[Export]
	public int Health { get; set; } = 1;
}
