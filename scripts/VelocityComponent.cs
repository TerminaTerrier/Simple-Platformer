using Godot;
using System;

public partial class VelocityComponent : Node2D
{
	[Export]
	private float maxSpeed = 100;
	[Export]
	GravityComponent gravityComponent;
	public Vector2 Velocity {get; set;}
	public override void _Ready()
	{
	}

	public void AccelerateInDirection(Vector2 direction)
	{
		AccelerateVelocity(direction * maxSpeed);
	}
	public void AccelerateVelocity(Vector2 velocity)
	{
		Velocity = Velocity.Lerp(velocity, 0.045f);
	}
	public void Decelerate()
	{
		AccelerateVelocity(Vector2.Zero);
	}

	public void AccelerateWithGravity()
	{
		Velocity = gravityComponent.ApplyGravity(Velocity);
	}

	public void Move(CharacterBody2D characterBody2D)
	{
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
		GD.Print(Velocity);
	}

	
}
