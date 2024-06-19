using Godot;
using System;

public partial class CounterDisplay : Label
{
	SignalBus signalBus;
	
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");
		signalBus.CounterUpdate += OnCounterUpdate;
		signalBus.GameOver += OnGameOver;
	}

	private void OnCounterUpdate(int count)
	{
		string display = "X " + count.ToString();
		this.Text = display;
	}

	private void OnGameOver()
	{
		this.Text = "";
	}
}
