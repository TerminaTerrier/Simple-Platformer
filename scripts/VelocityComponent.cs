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
	[Export]
	private float mass = 1;
	[Export]
	private bool includeGravity = false;
    Vector2 gravity = new Vector2 (0f, 1f);
	public float speedMultiplier { get; set; } = 1f;
	private float speedModifier = 1f;
	public float targetSpeed => maxSpeed * speedModifier * speedMultiplier;
	public Vector2 Velocity {get; set;}
	private Vector2 calculatedVelocity;
	
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
			 return new Vector2(0, 1f * targetSpeed); 
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
	public void ApplyGravity()
	{
		AccelerateInDirection(gravity, 1f);
	}

	public void AccelerateInDirection(Vector2 direction, float accScalar)
	{
		SumForce(direction * accScalar);
	}

	
	//combining gravity with direction leads to jump strength varying depending on whether the player is moving or idle
	public void AccelerateInDirectionWithGravity(Vector2 direction, Vector2 opposingForce)
	{
		//AccelerateVelocity(((direction + gravityComponent.GetGravity()) * targetSpeed) - opposingForce); //- opposing force
		//GD.Print(((direction + gravityComponent.GetGravity()) * targetSpeed) - opposingForce);
		//GD.Print(600 * GetProcessDeltaTime());
	}

	public void SumForce(Vector2 acceleration)
	{
		calculatedVelocity += (acceleration * mass) * (float)GetProcessDeltaTime();
		
	}
	public void AccelerateVelocity(Vector2 velocity, float accelerationRate)
	{
		
		//Velocity = Velocity.MoveToward(velocity, accelerationRate * (float)GetProcessDeltaTime());
		//GD.Print(Velocity.Lerp(velocity, 1f - (float)Math.Pow(accelerationRate, GetProcessDeltaTime()))); 
		//GD.Print(GetProcessDeltaTime());
		
	}
	
	
	
	
	public void Decelerate(Vector2 direction, float deccScalar)
	{	
		var speed = calculatedVelocity.X;
		
		if(Mathf.Max(speed, 0) > 0 || Mathf.Min(speed, 0) < 0)
		{
			AccelerateInDirection(direction, deccScalar);
		}
		GD.Print(speed);
	}

	public void DecelerateWithGravity(Vector2 opposingForce)
	{
		///AccelerateVelocity((gravityComponent.GetGravity() * targetSpeed) - opposingForce );
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
		//accelerationRate = newRate;
	}

	public void Move(CharacterBody2D characterBody2D)
	{
		Velocity = calculatedVelocity;
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
		GD.Print(Velocity);
	}

	
}
