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
		//bool directionSwitch = false;
		//if(GetLastSlideCollision() != null && directionSwitch == false)
		//{
		   var target = new Vector2(140, -4);
		   pathfindComponent.CallDeferred("SetTargetPosition", target);
		//	directionSwitch = true;
		//	GD.Print(target);
		//}
		//else if(GetLastSlideCollision() != null && directionSwitch == true)
		//{
		//	var target = GlobalPosition + Vector2.Left;
		//	pathfindComponent.CallDeferred("SetTargetPosition", target);
		//	directionSwitch = false;
		//	GD.Print(target);
		//}

		
		pathfindComponent.FollowPath();

		if(GetSlideCollisionCount() != 0)
		{
			velocityComponent.CollisionCheck(GetLastSlideCollision());
			velocityComponent.NormalForceCheck(GetLastSlideCollision());
		}
	
		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();
		//GD.Print(target);
	}
}
