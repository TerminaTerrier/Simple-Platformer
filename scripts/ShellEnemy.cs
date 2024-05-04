using Godot;
using System;

public partial class ShellEnemy : CharacterBody2D
{
	[Signal]
	public delegate void EnemyDeathEventHandler();
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	PathfindComponent pathfindComponent;
	[Export]
	CollisionHandler collisionHandler;
	[Export]
	RaycastComponent raycastComponent;
	[Export]
	HealthComponent healthComponent;
	bool directionSwitch = true;	
	StateMachine stateMachine = new();
	public override void _Ready()
	{
			
	
		healthComponent.OnDeath += Die;

		stateMachine.AddState(NormalState);
		stateMachine.Enter();
	
	}

	 public override void _PhysicsProcess(double delta)
    {
        stateMachine.Update();
		
    }

	private void NormalState()
	{
		 if(GetLastSlideCollision() != null)
		 {
		 	var collisionAngle = collisionHandler.GetCollisionAngle(GetLastSlideCollision());
		 
	     	if(directionSwitch == false)
		 	{
		 		var target = GlobalPosition +  new Vector2(100, 0);
		  	    pathfindComponent.CallDeferred("SetTargetPosition", target);
				raycastComponent.SetRaycastParamaters(GlobalPosition, GlobalPosition + new Vector2(12, 0));
				if(raycastComponent.GetRayCastQuery().Count != 0)
				{
		    		directionSwitch = true;
				}
		
		    	//GD.Print(raycastComponent.GetRayCastQuery().Count);
		 	}
		 	
			if(directionSwitch == true)
		 	{
				var target = GlobalPosition + new Vector2(-100, 0);
		 		pathfindComponent.CallDeferred("SetTargetPosition", target);
				raycastComponent.SetRaycastParamaters(GlobalPosition, GlobalPosition + new Vector2(-12, 0));
				if(raycastComponent.GetRayCastQuery().Count != 0)
				{
		    		directionSwitch = false;
				}
				
		 		//GD.Print(raycastComponent.GetRayCastQuery().Count);
		 	}
			//GD.Print(directionSwitch);
			
		 }
		
		pathfindComponent.FollowPath();

		if(GetSlideCollisionCount() != 0)
		{
			velocityComponent.CollisionCheck(GetLastSlideCollision());
			if(collisionHandler.CheckCollisionObjectType(collisionHandler.GetCollisionObject(GetLastSlideCollision()), typeof(TileMap)))
			{
			velocityComponent.NormalForceCheck(collisionHandler.GetCollisionObject(GetLastSlideCollision()), collisionHandler.GetCollisionPosition(GetLastSlideCollision()), collisionHandler.GetCollisionAngle(GetLastSlideCollision()));
			}
		}
	
		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();	

		GD.Print(healthComponent.Health);

		if(healthComponent.Health == 1)
		{
			//stateMachine.AddState(HideState);
			//stateMachine.Enter();
		}
	}

	private void HideState()
	{
		GD.Print(healthComponent.Health);
		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();
	}
	private void Die()
	{
		QueueFree();
	}

	
}
