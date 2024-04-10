using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class GravityComponent : Node2D
{
	[Export]
	public float Mass;
	private Vector2 Gravity;

	public void CalculateGravity()
	{
		Gravity = Vector2.Down * Mass;
	}

	public Vector2 ApplyGravity(Vector2 velocity)
	{
		return velocity + Gravity;
	}
}
