using Godot;
using System;

public partial class player : CharacterBody2D
{
	[Export]
	velocity_component velocityComponent;
	[Export]
	player_controller playerController;
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