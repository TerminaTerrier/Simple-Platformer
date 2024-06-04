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
	public delegate void LifeLostEventHandler(int lives); //emits when the player loses a life -- used to update life display
	[Signal]
	public delegate void SpecialBoxEventHandler(Vector2I spawnPosition); //emits when player hits a special box from below
	[Signal]
	public delegate void PowerUpEventHandler(int powerUpID); //emits when player picks up power-up
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
	public delegate void TimerUpdateEventHandler(double time); //emits in process method -- used to update time display
	[Signal]
	public delegate void GlobalTimeoutEventHandler(); //emits when timer reaches zero

}
