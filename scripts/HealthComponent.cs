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
	private Boolean didHit;
	SignalBus signalBus;
	public override void _Ready()
	{
		Health = stats.StartingHealth;
		GD.Print(Health);
		hurtBox.HitByHitbox += OnHitByHitbox;

		
	}

	public void OnHitByHitbox(HitboxComponent hitBox)
	{
		if(didHit == false)
		{
		didHit = true;
		CalculateHealth(-hitBox.stats.Damage);
		}
		
		
	}

    private void CalculateHealth(int healthUpdate)
	{
		if(Health < stats.MaxHealth)
		{
			Health = Health + healthUpdate;
			didHit = false;
		}

		if(Health <= 0)
		{
			EmitSignal("OnDeath");
		}
		
		
		GD.Print(Health);
	}
}
