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
		
		if(counter > 99)
		{
			signalBus.EmitSignal(SignalBus.SignalName.CounterRollover);
			counter = 0;
		}

		signalBus.EmitSignal(SignalBus.SignalName.CounterUpdate, counter);
	}
}
