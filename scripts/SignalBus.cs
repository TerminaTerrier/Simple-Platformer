using Godot;
using System;

public partial class SignalBus : Node
{
	[Signal]
	public delegate void StartGameEventHandler(); //emits when start button is clicked on main menu -- alerts levelloader to load a level


}
