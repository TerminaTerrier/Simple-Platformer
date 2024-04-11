using Godot;
using System;


public partial class VelocityComponent : Node2D
{
	[Export]
	GravityComponent gravityComponent;
	[Export]
	private float maxSpeed = 100;	
	public float speedMultiplier { get; set; } = 1f;
	private float speedModifier = 1f;
	public float targetSpeed => maxSpeed * speedModifier * speedMultiplier;
	private float accelerationWeight = 0.045f;
	private float decelerationWeight = 0.045f;
	
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
		Velocity = Velocity.Lerp(velocity, accelerationWeight); //should use accelerationWeight as  a parameter when fully implemented
	}
	public void BurstAccelerate(Vector2 velocity)
	{
		Velocity = Velocity + velocity;
	}
	public void Decelerate()
	{
		Velocity = Velocity.Lerp(Vector2.Zero, decelerationWeight);
	}

	public void AccelerateWithGravity()
	{
		var velocity = gravityComponent.ApplyGravity(Velocity);
		AccelerateVelocity(velocity);
	}

	public void SetMaxSpeed(float newSpeed)
	{
		maxSpeed = newSpeed;
	}

	public void SetSpeedModifier(float newModifier)
	{
		speedModifier = newModifier;
	}

	public void SetAccelerationWeight(float newWeight)
	{
		accelerationWeight = newWeight;
	}

	public void Move(CharacterBody2D characterBody2D)
	{
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
		GD.Print(Velocity);
	}

	
}
