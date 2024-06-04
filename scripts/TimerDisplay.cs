using Godot;
using System;

public partial class TimerDisplay : Label
{	
	SignalBus signalBus;
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		signalBus.TimerUpdate += OnTimerUpdate;
		signalBus.GameOver += OnGameOver;
	}

	private void OnTimerUpdate(double time)
	{
		int timeInteger = (int)time;
		if (timeInteger != 0)
		{
		this.Text = "TIME - " + timeInteger.ToString();
		}
	}

	private void OnGameOver()
	{
		this.Text = "";
	}

	
}
