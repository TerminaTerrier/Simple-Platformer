using Godot;
using System;

public partial class DamageOrb : CharacterBody2D
{
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	HealthComponent healthComponent;
	[Export]
	HitboxComponent hitboxComponent;
	[Export]
	HurtboxComponent hurtboxComponent;
	[Export]
	Sprite2D sprite;
	StateMachine stateMachine = new();
	SignalBus signalBus;
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");
		healthComponent.Death += Die;
		//signalBus.Warp += Die; - needs overload
		stateMachine.AddState(FallingState);
		stateMachine.Enter();
	}

    public override void _PhysicsProcess(double delta)
    {
        stateMachine.Update();
    }

	private void FallingState()
	{
		velocityComponent.SetVelocity(new Vector2(0,50	));
		velocityComponent.Move(this);

		if(IsOnFloor() == true)
		{
			signalBus.EmitSignal(SignalBus.SignalName.SFX, "Orb_Explosion");
			stateMachine.AddState(ExplodingState);
			stateMachine.Enter();
		}
		
	}

	private void ExplodingState()
	{
		hurtboxComponent.Monitoring = false;

		Timer timer = new();
		AddChild(timer);

		timer.Start(4);

		hitboxComponent.Scale = new Vector2(1.2f,2f);
		sprite.Texture = GD.Load<Texture2D>("res://assets/art/orb_explosion.png");
		timer.Timeout += () => QueueFree();
	}
	private void Die()
	{
		QueueFree();
	}

}
