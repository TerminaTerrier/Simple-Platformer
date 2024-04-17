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

	public Vector2 OpposingForceCheck(Vector2 from, Vector2 to) 
	{
		raycastComponent.SetRaycastParamaters(from, to);
		var raycastResult = raycastComponent.GetRayCastQuery();
		
		if(raycastResult.Count != 0)
		{
			GodotObject collider = (GodotObject)raycastResult["collider"];
			string level1MetaData = (string)collider.GetMeta("LVL1_TileMap");

			if(level1MetaData == "PhysicsEnabled")
			{
			return new Vector2(0, gravityComponent.GetGravity().Y * targetSpeed); 
			}
			else
			{
				return Vector2.Zero;
			}
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
