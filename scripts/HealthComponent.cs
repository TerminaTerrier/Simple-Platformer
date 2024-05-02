using Godot;
using System;

public partial class HealthComponent : Node2D
{
	[Signal]
	public delegate void OnDeathEventHandler();
	[Export]
	public HurtboxComponent hurtBox;
	[Export]
	private Stats stats;
	public int Health {get; private set;}
	SignalBus signalBus;
	public override void _Ready()
	{
		hurtBox.HitByHitbox += OnHitByHitbox;

		Health = stats.StartingHealth;
	}

	public void OnHitByHitbox(HitboxComponent hitBox)
	{
		CalculateHealth(-hitBox.stats.Damage);
	}

	private void CalculateHealth(int healthUpdate)
	{
		if(Health < stats.MaxHealth)
		{
			Health = healthUpdate + healthUpdate;
		}

		if(Health <= 0)
		{
			EmitSignal("OnDeath");
		}

		GD.Print(Health);
	}
}
