using Godot;
using System;

public partial class velocity_component : Node2D
{
	[Export]
	private float maxSpeed = 100;
	public Vector2 Velocity {get; set;}
	public override void _Ready()
	{
	}

	public void GetVelocity(Vector2 direction)
	{
		Velocity = direction * maxSpeed;
	}
	public void Move(CharacterBody2D characterBody2D)
	{
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
	}

	
}
