using Godot;
using System;

public partial class FallZone : Area2D
{
	SignalBus signalBus;
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");
		this.BodyEntered += OnBodyEntered;
	}

    private void OnBodyEntered(Node2D body)
	{
		signalBus.EmitSignal(SignalBus.SignalName.PitFall, body);
	}
	
}
