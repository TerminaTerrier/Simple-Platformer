using Godot;
using System;

public partial class SignalBus : Node
{
	[Signal]
	public delegate void StartGameEventHandler(); //emits when start button is clicked on main menu -- alerts UILoader to remove MainMenu & GameObjectLoader to load level one and player
	[Signal]
	public delegate void GameOverEventHandler(); //emits on player death -- frees current level and instantiates the Game Over menu

}
