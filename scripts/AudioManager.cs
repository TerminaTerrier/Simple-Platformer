using Godot;
using System;

public partial class AudioManager : AudioStreamPlayer2D
{
	AudioStream tokenSFX = GD.Load<AudioStream>("res://audio/SFX_token.ogg");
	AudioStream regPowerUpSFX = GD.Load<AudioStream>("res://audio/SFX_regular_power_up.ogg");
	AudioStream warpSFX = GD.Load<AudioStream>("res://audio/SFX_warp.ogg");
	SignalBus signalBus;
	
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		signalBus.SFX += (string sfxID) => 
		{
			switch (sfxID)
			{
				case "Token":
				this.Stream = tokenSFX;
				Play();
				break;
				case "PU-R":
				this.Stream = regPowerUpSFX;
				Play();
				break;
				case "Warp":
				this.Stream = warpSFX;
				Play();
				break;
				
			}
		};
	}

	
}
