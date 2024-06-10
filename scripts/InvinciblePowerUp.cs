using Godot;
using System;

public partial class InvinciblePowerUp : CharacterBody2D
{
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	Timer lifeTimer;
	SignalBus signalBus;
	StateMachine stateMachine = new();
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		stateMachine.AddState(MoveState);
		stateMachine.Enter();

		lifeTimer.Start(15);
		lifeTimer.Timeout += () => QueueFree();
	}

    public override void _PhysicsProcess(double delta)
    {
       stateMachine.Update();
    }
    private void MoveState()
	{
		velocityComponent.NormalForceCheck(this);
		velocityComponent.AccelerateInDirection(new Vector2(-1, 0), 25);

		if(IsOnFloor())
		{
			velocityComponent.AccelerateInDirection(Vector2.Up, 1200);
		}

		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();
	}

	private void OnBodyEntered(Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
		signalBus.EmitSignal(SignalBus.SignalName.PowerUp, 3);
		QueueFree();
		}
	}
}
	
	
