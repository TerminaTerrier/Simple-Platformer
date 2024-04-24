using Godot;
using System;

public partial class BasicEnemy : CharacterBody2D
{
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	PathfindComponent pathfindComponent;
	StateMachine stateMachine = new();
	public override void _Ready()
	{
		stateMachine.AddState(NormalState);
		stateMachine.Enter();
	}
	
	public override void _PhysicsProcess(double delta)
	{
		stateMachine.Update();
	}

	private void NormalState()
	{
		var target = GlobalPosition + Vector2.Left;
		pathfindComponent.CallDeferred("SetTargetPosition", target);
		pathfindComponent.FollowPath();

		velocityComponent.Move(this);
	}
}
