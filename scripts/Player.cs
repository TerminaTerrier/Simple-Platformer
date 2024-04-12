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
		velocityComponent.ApplyGravity();

		if(playerController.PressFlag == false)
		{
			//constant deceleration while there is no input!
			velocityComponent.Decelerate();
		}
		else
		{
			velocityComponent.AccelerateInDirection(playerController.direction);
		}

		

		playerController.JumpCheck(this.GlobalPosition, this.GlobalPosition + new Vector2(0, 15));

		velocityComponent.Move(this);
	}
}