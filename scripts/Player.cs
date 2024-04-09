using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	PlayerController playerController;
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
		
		velocityComponent.Move(this);
	}
}