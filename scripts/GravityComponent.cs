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
	private Vector2 Gravity =>  new Vector2(0, 9.8f);

	public override void _PhysicsProcess(double delta)
	{
		
	}

	
}
