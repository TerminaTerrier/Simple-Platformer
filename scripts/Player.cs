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
		

		if(playerController.PressFlag == false)
		{
			//constant deceleration while there is no input!
			velocityComponent.DecelerateWithGravity(gravityComponent.CalculatedGravity());
		}
		else
		{
			velocityComponent.AccelerateInDirectionWithGravity(playerController.direction, gravityComponent.CalculatedGravity());
		}

		velocityComponent.Move(this);
	}
}