using Godot;
using System;




public partial class VelocityComponent : Node2D
{
	[Export]
	RaycastComponent raycastComponent;
	[Export]
	private float maxSpeed = 100;	
	[Export]
	private float mass = 1;
	[Export]
	private bool includeGravity = false;
    Vector2 gravity = new Vector2 (0f, 10f);
	public float speedMultiplier { get; set; } = 1f;
	private float speedModifier = 1f;
	public float targetSpeed => maxSpeed * speedModifier * speedMultiplier;
	public Vector2 Velocity {get; set;}
	private Vector2 calculatedVelocity;
	private KinematicCollision2D kinematicCollision2D;
	
	public override void _Ready()
	{
		
	}

	public void ApplyGravity()
	{
			SumForce(gravity);
	}

	public void CollisionCheck(KinematicCollision2D collisionData)
	{
		var collisionVelocity = collisionData.GetColliderVelocity();
		calculatedVelocity += collisionVelocity;
	}
	
	public void NormalForceCheck(KinematicCollision2D collisionData)  
	{
		//I should think of a way to make it so that this class is not responsible for detecting the tilemap
		var collisionObject = collisionData.GetCollider();
		var collisionPosition = collisionData.GetPosition();
		var collisionAngle = Mathf.Round(collisionData.GetAngle());

		var tileMap = (TileMap)collisionObject;
		var tilePosition = tileMap.LocalToMap(tileMap.ToLocal(collisionPosition));
		var tile = tileMap.GetCellAtlasCoords(0, tilePosition);

		 switch (collisionAngle)
		 {
			  case 0:
				if(tile == new Vector2I(0, 0) | tile == new Vector2I(1, 0) | tile == new Vector2I(2, 0))
				{
					
					calculatedVelocity.Y *= 0.4f;
				}
				break;
			  case 3:
			    if(tile == new Vector2I(0, 0) | tile == new Vector2I(1, 0) | tile == new Vector2I(2, 0) | tile == new Vector2I(-1,-1))
				{
					
					calculatedVelocity.Y += 50f;
				}
				break;
			  case 2:
				if(tile == new Vector2I(0, 0) | tile == new Vector2I(1, 0) | tile == new Vector2I(2, 0) | tile == new Vector2I(-1,-1))
				{
					calculatedVelocity.X *= 0.4f;
				}
				break;
			  
		}

		//GD.Print(collisionAngle);
		//GD.Print(tilePosition);
		GD.Print(tile);
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
	
	
	
	
	public void Decelerate()
	{	
		var speed = calculatedVelocity.X;
		
		if(Mathf.Max(speed, 0) > 0 | Mathf.Min(speed, 0) < 0)
		{
			calculatedVelocity.X *= (5.5f * (float)GetProcessDeltaTime()) * mass;
		}
		//GD.Print(GetProcessDeltaTime());
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
		Velocity = calculatedVelocity.Clamp(new Vector2(-maxSpeed, -maxSpeed), new Vector2(maxSpeed, maxSpeed));
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
		GD.Print(Velocity);

	}

	
}
