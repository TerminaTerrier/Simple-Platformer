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
			velocityComponent.SetAccelerationRate(0.0003f);
			velocityComponent.DecelerateWithGravity(velocityComponent.OpposingForceCheck(GlobalPosition, GlobalPosition + new Vector2(0, 12)));
		}
		else
		{
			velocityComponent.SetAccelerationRate(0.0000005f);
			velocityComponent.AccelerateInDirectionWithGravity(playerController.direction, velocityComponent.OpposingForceCheck(GlobalPosition, GlobalPosition + new Vector2(0, 12)));
		}

		velocityComponent.Move(this);

		GD.Print(Engine.GetFramesPerSecond());
	}
}