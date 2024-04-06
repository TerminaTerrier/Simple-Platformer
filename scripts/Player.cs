using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	PlayerController playerController;
	public override void _Ready()
	{

	}

    public override void _Process(double delta)
    {
        RegularState();
    }

	public void RegularState()
	{
		velocityComponent.GetVelocity(playerController.direction);
		velocityComponent.Move(this);
	}
}