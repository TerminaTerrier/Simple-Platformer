using Godot;
using System;
using System.Collections;

public partial class StateMachine : Node
{
	public Action currentState;
	Stack statesStack = new Stack();
	public void Enter()
	{
		currentState = (Action)statesStack.Peek();
	}

	
	public void Update()
	{
		currentState();
	}

	public void AddState(Action state)
	{
		statesStack.Push(state);
	}

	public void Exit()
	{
		statesStack.Clear();
	}
}
