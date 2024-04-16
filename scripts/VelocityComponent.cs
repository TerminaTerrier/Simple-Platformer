using Godot;
using System;


public partial class VelocityComponent : Node2D
{
	[Export]
	GravityComponent gravityComponent;
	[Export]
	private float maxSpeed = 100;	
	[Export]
	private double accelerationRate = 0.05;
	public float speedMultiplier { get; set; } = 1f;
	private float speedModifier = 1f;
	public float targetSpeed => maxSpeed * speedModifier * speedMultiplier;
	public Vector2 Velocity {get; set;}
	public override void _Ready()
	{
	
	}

	public void OpposingForceCheck()
	{
		
	}
	
	public void AccelerateInDirection(Vector2 direction)
	{
		AccelerateVelocity(direction * targetSpeed);
	}

	//it still takes a second to get to max speed for gravity
	public void AccelerateInDirectionWithGravity(Vector2 direction)
	{
		AccelerateVelocity((direction + gravityComponent.GetGravity()) * targetSpeed);
		GD.Print((direction + gravityComponent.GetGravity()) * targetSpeed);
	}

	public void AccelerateVelocity(Vector2 velocity)
	{
		
		Velocity = Velocity.Lerp(velocity, 1f - (float)Math.Pow(accelerationRate, GetProcessDeltaTime())); 
		//GD.Print((float)Math.Pow(accelerationRate, GetProcessDeltaTime())); 
		//GD.Print(GetProcessDeltaTime());
	}
	
	
	
	public void Decelerate()
	{
		AccelerateVelocity(Vector2.Zero);
	}

	public void DecelerateWithGravity()
	{
		AccelerateVelocity(gravityComponent.GetGravity() * targetSpeed);
	}

	public void SetMaxSpeed(float newSpeed)
	{
		maxSpeed = newSpeed;
	}

	public void SetSpeedModifier(float newModifier)
	{
		speedModifier = newModifier;
	}

	public void SetAccelerationRate(float newRate)
	{
		accelerationRate = newRate;
	}

	public void Move(CharacterBody2D characterBody2D)
	{
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
		GD.Print(Velocity);
	}

	
}
