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
	[Export]
	HitboxComponent hitboxComponent;
	//turn off hitbox during hide
	[Export]
	HurtboxComponent hurtboxComponent;
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
		//GD.Print(healthComponent.Health);
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

		//GD.Print(healthComponent.Health);

		if(healthComponent.Health == 1)
		{
			stateMachine.AddState(HideState);
			stateMachine.Enter();
		}
	}

	private void HideState()
	{
		hitboxComponent.Monitorable = false;
		hurtboxComponent.Monitoring = false;

		//GD.Print(healthComponent.Health);
		var timer = GetTree().CreateTimer(5);

		
		
		var target = GlobalPosition;
		pathfindComponent.CallDeferred("SetTargetPosition", target);	
		pathfindComponent.FollowPath();
		
		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();

		timer.Timeout += () => { healthComponent.SetHealth(2); hitboxComponent.Monitorable = true; hurtboxComponent.Monitoring = true; stateMachine.AddState(NormalState); stateMachine.Enter(); }; 

		
	}
	private void Die()
	{
		QueueFree();
	}

	
}
