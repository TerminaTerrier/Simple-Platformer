using Godot;
using System;


public partial class VelocityComponent : Node2D
{
	[Export]
	GravityComponent gravityComponent;
	[Export]
	RaycastComponent raycastComponent;
	[Export]
	private float maxSpeed = 100;	
	private double accelerationRate;
	public float speedMultiplier { get; set; } = 1f;
	private float speedModifier = 1f;
	public float targetSpeed => maxSpeed * speedModifier * speedMultiplier;
	public Vector2 Velocity {get; set;}
	public override void _Ready()
	{
	
	}

	public Vector2 OpposingForceCheck(Vector2 from, Vector2 to) //will need to be able to differentiate between collision objects in the future
	{
		raycastComponent.SetRaycastParamaters(from, to);

		if(raycastComponent.GetRayCastQuery().Count != 0)
		{
			return new Vector2(0, 45); //this must be a modifiable value based on gravity
		}
		else
		{
			return new Vector2(0,0);
		}
		
	}
	
	public void AccelerateInDirection(Vector2 direction)
	{
		AccelerateVelocity(direction * targetSpeed);
	}

	//it still takes a second to get to max speed for gravity, this can cause the player to jump very high at random intervals
	public void AccelerateInDirectionWithGravity(Vector2 direction, Vector2 opposingForce)
	{
		AccelerateVelocity(((direction + gravityComponent.GetGravity()) * targetSpeed) - opposingForce); //- opposing force
		//GD.Print(((direction + gravityComponent.GetGravity()) * targetSpeed) - opposingForce);
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

	public void DecelerateWithGravity(Vector2 opposingForce)
	{
		AccelerateVelocity((gravityComponent.GetGravity() * targetSpeed) - opposingForce );
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
