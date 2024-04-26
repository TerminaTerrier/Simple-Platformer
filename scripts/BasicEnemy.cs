using Godot;
using System;

public partial class BasicEnemy : CharacterBody2D
{
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	PathfindComponent pathfindComponent;
	[Export]
	CollisionHandler collisionHandler;
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
		bool directionSwitch = false;

		if(GetLastSlideCollision() != null)
		{
		var collisionObject = collisionHandler.GetCollisionObject(GetLastSlideCollision());

		GD.Print(collisionHandler.CheckCollisionObjectType(collisionObject, typeof(CharacterBody2D)));

		//var tileMap = collisionHandler.CastCollisionObject<TileMap>(collisionObject);

		//GD.Print(tileMap is TileMap);
		}
		

		 if(GetLastSlideCollision() != null)
		 {
		 var collisionAngle = collisionHandler.GetCollisionAngle(GetLastSlideCollision());
		 
	     if(collisionAngle == 2 && directionSwitch == false)
		 {
		   var target = GlobalPosition + Vector2.Right;
		   pathfindComponent.CallDeferred("SetTargetPosition", target);
		   directionSwitch = true;
		//	GD.Print(target);
		 }
		 else if(collisionAngle == 2 && directionSwitch == true)
		 {
			var target = GlobalPosition + Vector2.Left;
		 	pathfindComponent.CallDeferred("SetTargetPosition", target);
		    directionSwitch = false;
		 	//GD.Print(target);
		 }
		 }
		
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
