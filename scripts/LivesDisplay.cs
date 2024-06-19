using Godot;
using System;

public partial class LivesDisplay : Label
{
	SignalBus signalBus;
	
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");
		signalBus.LivesUpdate += UpdateDisplay;
		signalBus.GameOver += OnGameOver;
	}

	private void UpdateDisplay(int lives)
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
