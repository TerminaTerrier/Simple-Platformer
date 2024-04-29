using Godot;
using System;
using System.Net.NetworkInformation;

public partial class Player : CharacterBody2D
{
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	PlayerController playerController;
	[Export]
	CollisionHandler collisionHandler;
	StateMachine stateMachine = new();
	public override void _Ready()
	{
		stateMachine.AddState(IdleState);
		stateMachine.Enter();
	}

    public override void _PhysicsProcess(double delta)
    {
        stateMachine.Update();
		
    }

	private void IdleState()
	{
		if(GetSlideCollisionCount() != 0)
		{	
			if(collisionHandler.CheckCollisionObjectType(GetLastSlideCollision(), typeof(TileMap)))
			{
				velocityComponent.NormalForceCheck(collisionHandler.GetCollisionObject(GetLastSlideCollision()), collisionHandler.GetCollisionPosition(GetLastSlideCollision()), collisionHandler.GetCollisionAngle(GetLastSlideCollision()));
			}
			velocityComponent.CollisionCheck(GetLastSlideCollision());
		}
		
		if(playerController.PressFlag == false)
		{
			velocityComponent.Decelerate();
		}
		else
		{
			stateMachine.AddState(WalkState);
			stateMachine.Enter();
		}

		velocityComponent.Move(this);
		//gravity must be called last to avoid it being added to velocity before a normal force check is completed
		velocityComponent.ApplyGravity();
		
	}
	private void WalkState()
	{
		if(GetSlideCollisionCount() != 0)
		{
			if(collisionHandler.CheckCollisionObjectType(GetLastSlideCollision(), typeof(TileMap)))
			{
				velocityComponent.NormalForceCheck(collisionHandler.GetCollisionObject(GetLastSlideCollision()), collisionHandler.GetCollisionPosition(GetLastSlideCollision()), collisionHandler.GetCollisionAngle(GetLastSlideCollision()));
			}
			velocityComponent.CollisionCheck(GetLastSlideCollision());
		}
	
		
		
		if(playerController.PressFlag == false)
		{
			stateMachine.AddState(IdleState);
			stateMachine.Enter();
		}
		else if(playerController.PressFlag == true)
		{
			velocityComponent.AccelerateInDirection(playerController.direction, 15f);
		}
		
		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();
		
	}

	
}