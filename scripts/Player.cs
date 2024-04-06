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

    public override void _Process(double delta)
    {
        stateMachine.Update();
    }

	public void RegularState()
	{
		velocityComponent.GetVelocity(playerController.direction);
		velocityComponent.Move(this);
	}
}