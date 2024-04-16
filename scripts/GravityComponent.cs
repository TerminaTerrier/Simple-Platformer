using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

public partial class GravityComponent : Node2D
{
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	public float Mass;
	private Vector2 Gravity;

    public void CalculatedGravity()
	{
		Gravity = Vector2.Down * Mass;
	}

	public void ApplyGravity()
	{
		//velocityComponent.AccelerateInDirection()
	}

	
}
