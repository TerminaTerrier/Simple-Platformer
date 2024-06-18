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
		if(body.IsInGroup("Player"))
		{
			signalBus.EmitSignal(SignalBus.SignalName.LevelComplete, nextLevelID);
				if(nextLevelID != 4)
				{
					signalBus.EmitSignal(SignalBus.SignalName.SFX, "Level_Clear");
				}
		}
	}

}
