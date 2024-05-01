using Godot;
using System;

public partial class HealthComponent : Node2D
{
	[Signal]
	public delegate void OnDeathEventHandler();
	[Export]
	public HurtboxComponent hurtBox;
	[Export]
	private int health = 1;
	public override void _Ready()
	{
		hurtBox.HitByHitbox += OnHitByHitbox;
	}

	public void OnHitByHitbox(HitboxComponent hitBox)
	{
		CalculateHealth(-hitBox.stats.Damage);
	}

	private void CalculateHealth(int healthUpdate)
	{
		health = healthUpdate + healthUpdate;

		if(health <= 0)
		{
			EmitSignal("OnDeath");
		}
		GD.Print(health);
	}
}
