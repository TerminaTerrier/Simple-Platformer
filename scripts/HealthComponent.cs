using Godot;
using System;

public partial class HealthComponent : Node2D
{
	[Signal]
	public delegate void DeathEventHandler();
	[Signal]
	public delegate void DamageEventHandler();
	[Export]
	public HurtboxComponent hurtBox;
	[Export]
	private Stats stats;
	public int Health {get; private set;}
	private Boolean didHit;
	SignalBus signalBus;
	
	public override void _EnterTree()
	{
		Health = stats.StartingHealth;
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

    public void CalculateHealth(int healthUpdate)
	{
		if(Health <= stats.MaxHealth)
		{
			GD.Print(Health);
			Health = Health + healthUpdate;
			didHit = false;
		}

		if(Health <= 0)
		{
			GD.Print("death");
			EmitSignal("Death");
			Health = stats.StartingHealth;
		}
		
		if(Health <= stats.MaxHealth && Health > 0)
		{
			EmitSignal("Damage");
		}
	}

    public void SetHealth(int healthUpdate)
	{
		Health = healthUpdate;
	}
	public void SetStartingHealth(int healthUpdate)
	{
		stats.StartingHealth = healthUpdate;
	}

    public override void _ExitTree()
    {
        hurtBox.HitByHitbox -= OnHitByHitbox;
    }
}
