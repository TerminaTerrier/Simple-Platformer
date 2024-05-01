using Godot;
using System;

public partial class HitboxComponent : Area2D
{
	[Export]
	public Stats stats;


	public override void _Ready()
	{
		AreaEntered += OnArea2DEntered;
	}

	public void OnArea2DEntered(Area2D otherArea)
	{
		
	}
}
