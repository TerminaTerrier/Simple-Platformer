using Godot;
using System;




public partial class VelocityComponent : Node2D
{
	[Export]
	private float maxSpeed = 100;	
	[Export]
	private float mass = 1;
	[Export]
	private float friction = 6f;
    Vector2 gravity = new Vector2 (0f, 15f);
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
			AddForce(gravity);
	}

	public void CollisionCheck(KinematicCollision2D collisionData)
	{
		var collisionVelocity = collisionData.GetColliderVelocity();
		calculatedVelocity -= collisionVelocity;
		//GD.Print(collisionVelocity);
		return;
	}
	
	public void NormalForceCheck(KinematicCollision2D collisionData)  
	{
		//I should think of a way to make it so that this class is not responsible for detecting the tilemap
		
		var collisionObject = collisionData.GetCollider();
		var collisionPosition = collisionData.GetPosition();
		var collisionAngle = Mathf.Round(collisionData.GetAngle());

		if(collisionObject.HasMeta("LVL1_TileMap"))
		{
		var tileMap = (TileMap)collisionObject;
		var tilePosition = tileMap.LocalToMap(tileMap.ToLocal(collisionPosition));
		var tile = tileMap.GetCellAtlasCoords(0, tilePosition);
		

		 switch (collisionAngle)
		 {
			  case 0:
				if(tile == new Vector2I(0, 0) | tile == new Vector2I(1, 0) | tile == new Vector2I(2, 0) | tile == new Vector2I(10, 7))
				{
					
					calculatedVelocity.Y *= 0.4f;
					GD.Print(tile);
				}
				break;
			  case 3:
			    if(tile == new Vector2I(0, 0) | tile == new Vector2I(1, 0) | tile == new Vector2I(2, 0) | tile == new Vector2I(-1,-1) | tile == new Vector2I(10, 7))
				{
					
					calculatedVelocity.Y += 50f;
					GD.Print(tile);
				}
				break;
			  case 2:
				if(tile == new Vector2I(0, 0) | tile == new Vector2I(1, 0) | tile == new Vector2I(2, 0) | tile == new Vector2I(-1,-1) | tile == new Vector2I(10, 7))
				{
					calculatedVelocity.X *= 0.4f;
					GD.Print(tile);
				}
				break;
			   
		}
		}
		//GD.Print(collisionAngle);
		GD.Print(collisionPosition);
		
	}
	

	public void AccelerateInDirection(Vector2 direction, float accScalar)
	{
		AddForce(direction * accScalar);
	}

	public void AddForce(Vector2 acceleration)
	{
		calculatedVelocity += (acceleration * mass) * (float)GetProcessDeltaTime();
	}

	public void Decelerate()
	{	
		var speed = calculatedVelocity.X;
		
		if(Mathf.Max(speed, 0) > 0 | Mathf.Min(speed, 0) < 0)
		{
			calculatedVelocity.X *= (friction * (float)GetProcessDeltaTime()) * mass;
		}
		//GD.Print(GetProcessDeltaTime());
	}

	public void SetMaxSpeed(float newSpeed)
	{
		maxSpeed = newSpeed;
	}

	public void SetSpeedModifier(float newModifier)
	{
	 
		speedModifier = newModifier;
	}


	public void Move(CharacterBody2D characterBody2D)
	{
		Velocity = calculatedVelocity.Clamp(new Vector2(-maxSpeed, -maxSpeed), new Vector2(maxSpeed, maxSpeed));
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
		//GD.Print(Velocity);
	}

	
}
