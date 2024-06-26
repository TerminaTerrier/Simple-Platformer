using Godot;
using System;

public partial class OffensivePowerUp : CharacterBody2D
{
	SignalBus signalBus;
	
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");
	}

	private void OnBodyEntered(Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
			signalBus.EmitSignal(SignalBus.SignalName.PowerUp, 2);
			signalBus.EmitSignal(SignalBus.SignalName.SFX, "PU-O");
			QueueFree();
		}
	}
}
