using Godot;
using System;




public partial class VelocityComponent : Node2D
{
	[Export]
	private float maxXSpeed = 100;	
	[Export]
	private float maxYSpeed = 200;
	[Export]
	private float mass = 1;
	[Export]
	private float friction = 6f;
    Vector2 gravity = new Vector2 (0f, 30f);
	public float speedMultiplier { get; set; } = 1f;
	private float speedModifier = 1f;
	public float targetSpeed => maxXSpeed * speedModifier * speedMultiplier;
	public Vector2 Velocity {get; set;}
	private Vector2 calculatedVelocity;
	private KinematicCollision2D kinematicCollision2D;
	
	public override void _Ready()
	{
		
	}
	
	public void NormalForceCheck(GodotObject collisionObject, Vector2 collisionPosition, float collisionAngle)  
	{
		//I should think of a way to make it so that this class is not responsible for detecting the tilemap
		//GD.Print(collisionAngle);
		
		var tileMap = (TileMap)collisionObject;
		var tilePosition = tileMap.LocalToMap(tileMap.ToLocal(collisionPosition));
		var tile = tileMap.GetCellAtlasCoords(0, tilePosition);
		
		
		 switch (collisionAngle)
		 {
			//normal force doesn't get applied when collisions from two different angles (from the side and below) are occuring but it doesn't seem like a problem 
			//replace with IsOnFloor()?
			  case 0:
				if(tile == new Vector2I(0, 0) | tile == new Vector2I(1, 0) | tile == new Vector2I(2, 0) | tile == new Vector2I(10, 7))
				{
					
					calculatedVelocity.Y *= 0.4f;
					//GD.Print("true");
					//GD.Print(tile);
				}
				break;
			  case 3:
			    if(tile == new Vector2I(0, 0) | tile == new Vector2I(1, 0) | tile == new Vector2I(2, 0) | tile == new Vector2I(-1,-1) | tile == new Vector2I(10, 7))
				{
					
					calculatedVelocity.Y += 50f;
					//GD.Print(tile);
				}
				break;
			  case 2:
				if(tile == new Vector2I(0, 0) | tile == new Vector2I(1, 0) | tile == new Vector2I(2, 0) | tile == new Vector2I(-1,-1) | tile == new Vector2I(10, 7))
				{
					calculatedVelocity.X *= 0.4f;
					//GD.Print(tile);
				}
				break;
			   
		}
		
		//GD.Print(collisionAngle);
		//GD.Print(collisionPosition);
		
	}
	
	public void ApplyGravity()
	{
			AddForce(gravity);
	}
	public void AccelerateInDirection(Vector2 direction, float accScalar)
	{
		AddForce(direction * accScalar);
	}

	public void SetVelocity(Vector2 newVelocity)
	{
		calculatedVelocity = newVelocity;
	}

	public void AddForce(Vector2 acceleration)
	{
		calculatedVelocity = calculatedVelocity.Clamp(new Vector2(-maxXSpeed, -maxYSpeed), new Vector2(maxXSpeed, maxYSpeed));
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

	public void SetMaxSpeed(float newXSpeed, float newYSpeed)
	{
		maxXSpeed = newXSpeed;
		maxYSpeed = newYSpeed;
	}

	public void SetSpeedModifier(float newModifier)
	{
	 
		speedModifier = newModifier;
	}

	
	public void Move(CharacterBody2D characterBody2D)
	{
		Velocity = calculatedVelocity.Clamp(new Vector2(-maxXSpeed, -maxYSpeed), new Vector2(maxXSpeed, maxYSpeed));
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
		//GD.Print(Velocity);
		//GD.Print(characterBody2D.Velocity);
	}

	public void CollisionCheck(KinematicCollision2D collisionData)
	{
		//GD.Print(collisionData.GetColliderVelocity());
		var collisionVelocity = collisionData.GetColliderVelocity();
		calculatedVelocity += new Vector2(collisionVelocity.X, 0);
		//GD.Print(collisionVelocity);
		
		
	}

	public Vector2 GetVelocity()
	{
		return Velocity;
	}
	
}
