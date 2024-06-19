using Godot;
using System;

public partial class AudioManager : AudioStreamPlayer2D
{
	AudioStream tokenSFX = GD.Load<AudioStream>("res://audio/SFX_token.ogg");
	AudioStream jumpSFX = GD.Load<AudioStream>("res://audio/SFX_jump.ogg");
	AudioStream regPowerUpSFX = GD.Load<AudioStream>("res://audio/SFX_regular_power_up.ogg");
	AudioStream offPowerUpSFX = GD.Load<AudioStream>("res://audio/SFX_offensive_power_up.ogg");
	AudioStream invcPowerUpSFX = GD.Load<AudioStream> ("res://audio/SFX_invincible_power_up.ogg");
	AudioStream orb_spawnSFX = GD.Load<AudioStream>("res://audio/SFX_orb_spawn.ogg");
	AudioStream orb_explosionSFX = GD.Load<AudioStream>("res://audio/SFX_orb_explosion.ogg");
	AudioStream warpSFX = GD.Load<AudioStream>("res://audio/SFX_warp.ogg");
	AudioStream squishSFX = GD.Load<AudioStream>("res://audio/SFX_squish.ogg");
	AudioStream shellSFX = GD.Load<AudioStream>("res://audio/SFX_shell.ogg");
	AudioStream brickSFX = GD.Load<AudioStream>("res://audio/SFX_brick.ogg");
	AudioStream special_blockSFX = GD.Load<AudioStream>("res://audio/SFX_special_block.ogg");
	AudioStream lvl_clearSFX = GD.Load<AudioStream>("res://audio/SFX_lvl_clear.ogg");
	AudioStream victorySFX = GD.Load<AudioStream>("res://audio/SFX_victory.ogg");
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
				case "Jump":
				    this.Stream = jumpSFX;
				    Play();
				break;
				case "PU-R":
				    this.Stream = regPowerUpSFX;
				    Play();
				break;
				case "PU-O":
				    this.Stream = offPowerUpSFX;
				    Play();
				break;
				case "PU-I":
				    this.Stream = invcPowerUpSFX;
				    Play();
				break;
				case "Orb_Spawn":
				    this.Stream = orb_spawnSFX;
				    Play();
				break;
				case "Orb_Explosion":
				    this.Stream = orb_explosionSFX;
				    Play();
				break;
				case "Warp":
				    this.Stream = warpSFX;
				    Play();
				break;
				case "Level_Clear":
				    this.Stream = lvl_clearSFX;
				    Play();
				break;
				case "Squish":
				    this.Stream = squishSFX;
				    Play();
				break;
				case "Shell":
				    this.Stream = shellSFX;
				    Play();
				break;
				case "Brick":
				    this.Stream = brickSFX;
				    Play();
				break;
				case "Special_Block":
				    this.Stream = special_blockSFX;
				    Play();
				break;
				case "Victory":
				    this.Stream = victorySFX;
				    Play();
				break;
			}
		};
	}

	
}
