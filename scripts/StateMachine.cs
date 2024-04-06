using Godot;
using System;

public partial class StateMachine : Node
{
	public Action currentState;
	public void Enter()
	{
	}

	
	public void Update()
	{
		currentState();
	}

	public void AddState(Action state)
	{
		currentState = state;
	}
}
