using Godot;
using System;

public partial class LevelEndGate : Area2D
{
	[Export]
	int nextLevelID;
	SignalBus signalBus;
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");
		this.BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if(body.Name == "Player")
		{
		signalBus.EmitSignal(SignalBus.SignalName.LevelComplete, nextLevelID);
		}
	}

}
