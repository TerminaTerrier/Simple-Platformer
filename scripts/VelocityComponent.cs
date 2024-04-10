using Godot;
using System;

public partial class VelocityComponent : Node2D
{
	[Export]
	GravityComponent gravityComponent;
	[Export]
	private float maxSpeed = 100;	
	public float speedMultiplier { get; set; } = 1f;
	public float targetSpeed => maxSpeed * speedMultiplier;
	public float accelerationWeight;
	public Vector2 Velocity {get; set;}
	public override void _Ready()
	{
	}

	public void AccelerateInDirection(Vector2 direction)
	{
		AccelerateVelocity(direction * targetSpeed);
	}
	public void AccelerateVelocity(Vector2 velocity)
	{
		Velocity = Velocity.Lerp(velocity, accelerationWeight);
	}
	public void Decelerate()
	{
		AccelerateVelocity(Vector2.Zero);
	}

	public void AccelerateWithGravity()
	{
		var velocity = gravityComponent.ApplyGravity(Velocity);
		AccelerateVelocity(velocity);
	}

	public void Move(CharacterBody2D characterBody2D)
	{
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
		GD.Print(Velocity);
	}

	
}
