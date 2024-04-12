using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

public partial class GravityComponent : Node2D
{
	[Export]
	public float Mass;
	private Vector2 Gravity;

    public Vector2 CalculatedGravity()
	{
		return Vector2.Down * Mass;
	}

	
}
