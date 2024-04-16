using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

public partial class GravityComponent : Node2D
{
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	private float Mass;
	private Vector2 Gravity =>  Vector2.Down * Mass;

	public void SetMass(float newMass)
	{
		Mass = newMass;
	}

	public Vector2 GetGravity()
	{
		return Gravity;
	}

	
}
