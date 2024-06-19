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
    Vector2 gravity;
	public float speedMultiplier { get; set; } = 1f;
	private float speedModifier = 1f;
	public float targetSpeed => maxXSpeed * speedModifier * speedMultiplier;
	public Vector2 Velocity {get; set;}
	private Vector2 calculatedVelocity;
	private KinematicCollision2D kinematicCollision2D;
	
	public void NormalForceCheck(CharacterBody2D characterBody2D)  
	{
		if(characterBody2D.IsOnFloor())
		{
			gravity = new Vector2(0,0);
			calculatedVelocity.Y *= 0.8f;
		}

		if(!characterBody2D.IsOnFloor())
		{
			gravity = new Vector2(0, 25);
		}
		
		if(characterBody2D.IsOnWall())
		{
			calculatedVelocity.X *= 0.4f;
		}

		if(characterBody2D.IsOnCeiling())
		{
			calculatedVelocity.Y += 50f;
		}
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
			calculatedVelocity.X *= friction * (float)GetProcessDeltaTime();
		}
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
	}

	public void CollisionCheck(KinematicCollision2D collisionData)
	{
		var collisionVelocity = collisionData.GetColliderVelocity();
		calculatedVelocity += new Vector2(collisionVelocity.X, 0);	
	}

	public Vector2 GetVelocity()
	{
		return Velocity;
	}
	
}
