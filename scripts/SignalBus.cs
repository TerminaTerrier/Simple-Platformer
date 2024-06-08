using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class SignalBus : Node
{
	[Signal]
	public delegate void StartGameEventHandler(); //emits when start button is clicked on main menu -- alerts UILoader to remove MainMenu & GameObjectLoader to load level one and player
	[Signal]
	public delegate void GameOverEventHandler(); //emits on player death -- frees current level and instantiates the Game Over menu
	[Signal]
	public delegate void LivesUpdateEventHandler(int lives); //emits when the player loses a life -- used to update life display
	[Signal]
	public delegate void SpecialBoxEventHandler(Vector2I spawnPosition, int powerUpState); //emits when player hits a special box from below
	[Signal]
	public delegate void PowerUpEventHandler(int powerUpID); //emits when player picks up power-up
	[Signal]
	public delegate void SpecialActionEventHandler(Vector2 position); //emits when player presses the up input when in the offensive state
	[Signal]
	public delegate void BrickHitEventHandler(Vector2 position); //emits when player hits breakable tiles when powered-up
	[Signal]
	public delegate void WarpZoneEnterEventHandler(int warpVal, Vector2 telePosition); //emits when player enters warp zone
	[Signal]
	public delegate void WarpZoneExitEventHandler(); //emits when player exits warp zone
	[Signal]
	public delegate void WarpEventHandler(int warpVal, Vector2 telePosition); //emits when player warps
	[Signal]
	public delegate void CollectEventHandler(); //emits when player collects a token
	[Signal]
	public delegate void CounterUpdateEventHandler(int count); //emits when counter is updated
	[Signal]
	public delegate void CounterRolloverEventHandler(); //emits when counter reaches 100 -- notifies player script to increase lives by 1
	[Signal]
	public delegate void TimerUpdateEventHandler(double time); //emits in process method -- used to update time display
	[Signal]
	public delegate void GlobalTimeoutEventHandler(); //emits when timer reaches zero
	[Signal]
	public delegate void LevelCompleteEventHandler(int levelID); //emits when player touches the level end gate
	[Signal]
	public delegate void PitFallEventHandler(Node2D body); //emits when a body enters a fall zone
	[Signal]
	public delegate void SFXEventHandler(string sfxID); //emits when a sound effect needs to be played
}
