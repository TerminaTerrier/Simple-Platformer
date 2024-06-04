using Godot;
using System;

public partial class TokenTexture : TextureRect
{
	SignalBus signalBus;
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");
		signalBus.GameOver += OnGameOver;
	}

	private void OnGameOver()
	{
		this.Texture = null;
	}
	
}
