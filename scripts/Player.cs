using Godot;
using System;
using System.Net.NetworkInformation;

public partial class Player : CharacterBody2D
{
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	PlayerController playerController;
	StateMachine stateMachine = new();
	public override void _Ready()
	{
		stateMachine.AddState(WalkState);
		stateMachine.Enter();
	}

    public override void _PhysicsProcess(double delta)
    {
        stateMachine.Update();
		
    }

	public void IdleState()
	{
		if(GetSlideCollisionCount() != 0)
		{
		 	velocityComponent.CollisionCheck(GetLastSlideCollision());
			velocityComponent.NormalForceCheck(GetLastSlideCollision());
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
	public void WalkState()
	{
		if(GetSlideCollisionCount() != 0)
		{
			velocityComponent.CollisionCheck(GetLastSlideCollision());
			velocityComponent.NormalForceCheck(GetLastSlideCollision());
		}
	
		
		
		if(playerController.PressFlag == false)
		{
			stateMachine.AddState(IdleState);
			stateMachine.Enter();
		}
		else if(playerController.PressFlag == true)
		{
			velocityComponent.AccelerateInDirection(playerController.direction, 10f);
		}
		
		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();
		
	}

	
}