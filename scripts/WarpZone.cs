using Godot;
using System;

public partial class WarpZone : Area2D
{
	[Export]
	int warpVal;
	[Export]
	Vector2 telePosition;
	SignalBus signalBus;

	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");
		this.BodyEntered += OnBodyEntered;
		this.BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
			signalBus.EmitSignal(SignalBus.SignalName.WarpZoneEnter, warpVal, telePosition);
			GD.Print("Warp ready!");
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
			signalBus.EmitSignal(SignalBus.SignalName.WarpZoneExit);
		}
	}
}
