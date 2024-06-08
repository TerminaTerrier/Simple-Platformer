using Godot;
using System;

public partial class MusicPlayer : AudioStreamPlayer
{
	AudioStream level1Theme = GD.Load<AudioStream>("res://audio/lvl1.wav");
	SignalBus signalBus;
	
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		signalBus.StartGame += () => {this.Stream = level1Theme; Play();}; 
	}


}
