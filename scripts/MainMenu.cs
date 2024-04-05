using Godot;
using System;

public partial class MainMenu : Control
{
	SignalBus signalBus;
	
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");
	}

	public void OnButtonPressed()
	{
		signalBus.EmitSignal(SignalBus.SignalName.StartGame);
	}
}
