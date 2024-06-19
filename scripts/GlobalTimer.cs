using Godot;
using System;

public partial class GlobalTimer : Node
{
	SignalBus signalBus;
	Timer globalTimer;

	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		globalTimer = new();
		AddChild(globalTimer);

		signalBus.StartGame += () => globalTimer.Start(500f);
		signalBus.LevelComplete += (int levelID) => globalTimer.Start(500f);
		signalBus.GameOver += () => globalTimer.Stop();

		globalTimer.Timeout += () => signalBus.EmitSignal(SignalBus.SignalName.GlobalTimeout);
	}

    public override void _Process(double delta)
    {
        signalBus.EmitSignal(SignalBus.SignalName.TimerUpdate, globalTimer.TimeLeft);
    }


}
