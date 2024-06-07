using Godot;
using System;

public partial class Counter : Node
{
	public int counter {get; set;}
	SignalBus signalBus;
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		signalBus.Collect += CounterUpdate;

	}

	private void CounterUpdate()
	{
		counter++;
		signalBus.EmitSignal(SignalBus.SignalName.CounterUpdate, counter);

		if(counter == 100)
		{
			signalBus.EmitSignal(SignalBus.SignalName.CounterRollover);
			counter = 0;
		}
	}

	
}
