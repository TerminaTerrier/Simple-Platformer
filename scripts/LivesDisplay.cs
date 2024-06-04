using Godot;
using System;

public partial class LivesDisplay : Label
{
	SignalBus signalBus;
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		signalBus.LifeLost += OnLifeLost;
		signalBus.GameOver += OnGameOver;

	}

	private void OnLifeLost(int lives)
	{
		if(lives >= 0)
		{
			this.Text = "Lives: " + lives.ToString();
		}
	}

	private void OnGameOver()
	{
		this.Text = "";
	}
}
