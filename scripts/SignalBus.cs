using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class SignalBus : Node
{
	[Signal]
	public delegate void StartGameEventHandler(); //Emits when start button is clicked on main menu -- alerts UILoader to remove MainMenu & GameObjectLoader to load level one and player.
	[Signal]
	public delegate void GameOverEventHandler(); //Emits on player death -- frees current level and instantiates the Game Over menu.
	[Signal]
	public delegate void LivesUpdateEventHandler(int lives); //Emits when the player loses a life -- used to update life display.
	[Signal]
	public delegate void SpecialBoxEventHandler(Vector2I spawnPosition, int powerUpState); //Emits when player hits a special box from below.
	[Signal]
	public delegate void PowerUpEventHandler(int powerUpID); //Emits when player picks up power-up.
	[Signal]
	public delegate void SpecialActionEventHandler(Vector2 position); //Emits when player presses the up input when in the offensive state.
	[Signal]
	public delegate void BrickHitEventHandler(Vector2 position); //Emits when player hits breakable tiles when powered-up.
	[Signal]
	public delegate void WarpZoneEnterEventHandler(int warpVal, Vector2 telePosition); //Emits when player enters warp zone.
	[Signal]
	public delegate void WarpZoneExitEventHandler(); //Emits when player exits warp zone.
	[Signal]
	public delegate void WarpEventHandler(int warpVal, Vector2 telePosition); //Emits when player warps.
	[Signal]
	public delegate void CollectEventHandler(); //Emits when player collects a token.
	[Signal]
	public delegate void CounterUpdateEventHandler(int count); //Emits when counter is updated.
	[Signal]
	public delegate void CounterRolloverEventHandler(); //Emits when counter reaches 100 -- notifies player script to increase lives by 1.
	[Signal]
	public delegate void TimerUpdateEventHandler(double time); //Emits in process method -- used to update time display.
	[Signal]
	public delegate void GlobalTimeoutEventHandler(); //Emits when timer reaches zero.
	[Signal]
	public delegate void LevelCompleteEventHandler(int levelID); //Emits when player touches the level end gate.
	[Signal]
	public delegate void PitFallEventHandler(Node2D body); //Emits when a body enters a fall zone.
	[Signal]
	public delegate void SFXEventHandler(string sfxID); //Emits when a sound effect needs to be played.
	[Signal]
	public delegate void VictoryEventHandler(); //Emits when the player beats the game.
}
