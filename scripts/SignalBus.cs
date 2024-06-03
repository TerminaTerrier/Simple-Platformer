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
	public delegate void WarpEventHandler(int warpVal, Vector2 telePosition); //emits when player warps;


}
