using Godot;
using System;

public partial class Token : Area2D
{
	SignalBus signalBus;
	
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");
		this.BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
		    signalBus.EmitSignal(SignalBus.SignalName.Collect);
	        signalBus.EmitSignal(SignalBus.SignalName.SFX, "Token");
		    QueueFree();
		}
	}
}
