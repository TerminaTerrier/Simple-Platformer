using Godot;
using System;

public partial class MusicPlayer : AudioStreamPlayer
{
	AudioStream level1Theme = GD.Load<AudioStream>("res://audio/lvl1.wav");
	AudioStream level2Theme = GD.Load<AudioStream>("res://audio/lvl2.ogg");
	AudioStream level3Theme = GD.Load<AudioStream>("res://audio/lvl3.ogg");
	AudioStream sublevelTheme = GD.Load<AudioStream>("res://audio/sub_lvl.ogg");
	AudioStream streamHolder;
	SignalBus signalBus;
	
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		signalBus.StartGame += () => {this.Stream = level1Theme; streamHolder = this.Stream; Play();}; 

		signalBus.Victory += () => Stop();

		signalBus.LevelComplete += (int levelID) => 
		{
			switch(levelID)
			{
				case 2:
				this.Stream = level2Theme;
				streamHolder = this.Stream;
				Play();
				break;
				case 3:
				this.Stream = level3Theme;
				streamHolder = this.Stream;
				Play();
				break;
			}
		
		
		};

		signalBus.Warp += (int warpVal, Vector2 telePosition) => 
		{
			
			if(warpVal == -1 || warpVal == -2 || warpVal == -3)
			{
				this.Stream = sublevelTheme;
				Play();
			}
			else
			{
				this.Stream = streamHolder;
				Play();
			}
		};
	}


}
