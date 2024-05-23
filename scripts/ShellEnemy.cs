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
	[Export]
	RayCast2D upRaycast;
	bool directionSwitch = true;
	bool timeoutLock;	
	SceneTreeTimer hideTimer;
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
			  //GD.Print(GetLastSlideCollision().GetNormal());
		 }
		
		pathfindComponent.FollowPath();

		//if(GetSlideCollisionCount() != 0)
		//{
		//	velocityComponent.CollisionCheck(GetLastSlideCollision());
		//	if(collisionHandler.CheckCollisionObjectType(collisionHandler.GetCollisionObject(GetLastSlideCollision()), typeof(TileMap)))
		//	{
		//	velocityComponent.NormalForceCheck(collisionHandler.GetCollisionObject(GetLastSlideCollision()), collisionHandler.GetCollisionPosition(GetLastSlideCollision()), collisionHandler.GetCollisionAngle(GetLastSlideCollision()));
		//	}
		//}
	
		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();	

		
		//GD.Print(velocityComponent.GetVelocity());
		if(healthComponent.Health == 1)
		{
			//GD.Print(healthComponent.Health);
			//velocityComponent.SetVelocity(Vector2.Zero);

			hideTimer = GetTree().CreateTimer(10);
			//have do gate this because the entire state runs in a process loop -- need to figure out how to allow delegate states to execute outside of process
			if(timeoutLock == false)
			{
			hideTimer.Timeout += OnHideTimerTimeout;  
			timeoutLock = true;
			}

			stateMachine.AddState(HideState);
			stateMachine.Enter();

			hitboxComponent.Monitorable = false;
			hurtboxComponent.Monitoring = false;
			GD.Print("Entering Hide State");
		}
		//GD.Print(GetSlideCollisionCount());
		//GD.Print(hurtboxComponent.Monitoring = true);
		//GD.Print(healthComponent.Health);
		
	}

	private void HideState()
	{
		//GD.Print(GetLastSlideCollision());
		
		

		//GD.Print(healthComponent.Health);
		

	    //GD.Print(GetTree());
		//velocityComponent.CollisionCheck(GetLastSlideCollision());
		//if(GetSlideCollisionCount() != 0)
		//{
		//	velocityComponent.CollisionCheck(GetLastSlideCollision());
		//	if(collisionHandler.CheckCollisionObjectType(collisionHandler.GetCollisionObject(GetLastSlideCollision()), typeof(TileMap)))
		//	{
		//	velocityComponent.NormalForceCheck(collisionHandler.GetCollisionObject(GetLastSlideCollision()), collisionHandler.GetCollisionPosition(GetLastSlideCollision()), collisionHandler.GetCollisionAngle(GetLastSlideCollision()));
		//	}
		//}
		if(GetSlideCollisionCount() != 0)
		{
			Vector2 collisionDirection = GetLastSlideCollision().GetNormal();
			//GD.Print(collisionDirection);
			//GD.Print(GetSlideCollisionCount());
			switch(collisionDirection)
			{
				case (1,0):
				velocityComponent.SetVelocity(new Vector2(75, 0));
				hitboxComponent.Monitorable = true;
				break;
				case (-1,0):
				velocityComponent.SetVelocity(new Vector2(-75,0));
				hitboxComponent.Monitorable = true;
				break;

			}

			if(upRaycast.IsColliding())
			{
				if(velocityComponent.Velocity != Vector2.Zero )
				{
					velocityComponent.SetVelocity(Vector2.Zero);
				}
				else if(velocityComponent.Velocity == Vector2.Zero)
				{
					velocityComponent.SetVelocity(new Vector2(75, 0));
				}
			}
			
			//var collidierVelocity = GetLastSlideCollision().GetColliderVelocity();
			//var target = GlobalPosition + new Vector2(collidierVelocity.X, 0);
			//pathfindComponent.CallDeferred("SetTargetPosition", target);	
			//GD.Print(collidierVelocity);	
			GD.Print(GetLastSlideCollision().GetNormal());
		}

	

		//pathfindComponent.FollowPath();

		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();
		
		
		//GD.Print(velocityComponent.GetVelocity());
		
	}
	private void OnHideTimerTimeout()
	{
		GD.Print("Entering Normal State"); 
		//var callable = new Callable(this, MethodName.OnHideTimerTimeout);
		healthComponent.SetHealth(2); 

		hitboxComponent.Monitorable = true; 
		hurtboxComponent.Monitoring = true; 

		stateMachine.AddState(NormalState); 
		stateMachine.Enter();

		//Disconnect("Timeout", callable);
		timeoutLock = false;
	}
	private void Die()
	{
		QueueFree();
	}

	
}
