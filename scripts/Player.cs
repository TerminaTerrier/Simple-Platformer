using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	PlayerController playerController;
	[Export]
	GravityComponent gravityComponent;
	[Export]
	RaycastComponent raycastComponent;
	StateMachine stateMachine = new();
	public override void _Ready()
	{
		stateMachine.AddState(RegularState);
	}

    public override void _PhysicsProcess(double delta)
    {
        stateMachine.Update();
		
    }

	public void RegularState()
	{
		
		if(playerController.PressFlag == false)
		{
			velocityComponent.Decelerate();
		}
		else
		{
			velocityComponent.AccelerateInDirection(playerController.direction);
		}

		if(playerController.direction == Vector2.Up)
		{
			velocityComponent.accelerationWeight = 0.5f;
			
		}

		playerController.JumpCheck(this.GlobalPosition, this.GlobalPosition + new Vector2(0, 25));

		gravityComponent.CalculateGravity();

		velocityComponent.AccelerateWithGravity();

		velocityComponent.Move(this);
	}
}