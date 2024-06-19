using Godot;
using System;

public partial class ShellEnemy : CharacterBody2D
{
	[Signal]
	public delegate void EnemyDeathEventHandler();
	Resource awakeSprite = GD.Load<Resource>("res://assets/art/shell_enemy.png");
	Resource hideSprite = GD.Load<Resource>("res://assets/art/shell_enemy_hide.png");
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
	[Export]
	HurtboxComponent hurtboxComponent;
	[Export]
	NavigationAgent2D navAgent;
	[Export]
	RayCast2D upRaycast;
	[Export]
	RayCast2D leftRaycast;
	[Export]
	RayCast2D rightRaycast;
	[Export]
	Sprite2D sprite;
	bool directionSwitch = true;
	bool enemyStopped;
	bool timeoutLock;	
	SceneTreeTimer hideTimer;
	SignalBus signalBus;
	StateMachine stateMachine = new();

	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		healthComponent.Death += Die;
		signalBus.PitFall += OnPitfall;

		stateMachine.AddState(NormalState);
		stateMachine.Enter();
	}

	public override void _PhysicsProcess(double delta)
    {
        stateMachine.Update();
    }

	private void NormalState()
	{
		velocityComponent.NormalForceCheck(this);

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

				sprite.FlipH = true;
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

				sprite.FlipH = false;
		 	}
		}
		
		pathfindComponent.FollowPath();
	
		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();	

		if(healthComponent.Health == 1)
		{
			signalBus.EmitSignal(SignalBus.SignalName.SFX, "Shell");
			hideTimer = GetTree().CreateTimer(10);

			//Have do gate this because the entire state runs in a process loop -- need to figure out how to allow delegate states to execute outside of process.
			if(timeoutLock == false)
			{
			    hideTimer.Timeout += OnHideTimerTimeout;  
			    timeoutLock = true;
			}

			sprite.Texture = (Texture2D)hideSprite;

			stateMachine.AddState(HideState);
			stateMachine.Enter();

			hitboxComponent.Monitorable = false;
			hurtboxComponent.SetCollisionMaskValue(2, false);
		}
		
	}

	private void HideState()
	{
		velocityComponent.NormalForceCheck(this);

		if(GetSlideCollisionCount() != 0)
		{
			Vector2 collisionDirection = GetLastSlideCollision().GetNormal();
	
			switch(collisionDirection)
			{
				case (1,0):
				    signalBus.EmitSignal(SignalBus.SignalName.SFX, "Shell");
				    velocityComponent.SetVelocity(new Vector2(75, 0));
				    hitboxComponent.Monitorable = true;
				    sprite.FlipH = true;
				break;
				case (-1,0):
				    signalBus.EmitSignal(SignalBus.SignalName.SFX, "Shell");
				    velocityComponent.SetVelocity(new Vector2(-75,0));
				    hitboxComponent.Monitorable = true;
				    sprite.FlipH = false;
				break;
			}

			if(upRaycast.IsColliding() | enemyStopped == true)
			{
				hitboxComponent.Monitorable = false;
				enemyStopped = true;

				if(velocityComponent.Velocity != Vector2.Zero )
				{
					velocityComponent.SetVelocity(Vector2.Zero);
				}
				
				if(enemyStopped == true)
				{
					if(upRaycast.IsColliding() | leftRaycast.IsColliding() | rightRaycast.IsColliding() && velocityComponent.Velocity == Vector2.Zero)
					{
						signalBus.EmitSignal(SignalBus.SignalName.SFX, "Shell");
						velocityComponent.SetVelocity(new Vector2(75, 0));
						hitboxComponent.Monitorable = true;
						enemyStopped = false;
					}
				}
				
			}
			
		
		}

		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();
	}
	private void OnHideTimerTimeout()
	{
		healthComponent.SetHealth(2); 

		hitboxComponent.Monitorable = true; 
		hurtboxComponent.SetCollisionMaskValue(2, true);
		sprite.Texture = (Texture2D)awakeSprite;

		stateMachine.AddState(NormalState); 
		stateMachine.Enter();

		timeoutLock = false;
	}
	private void Die()
	{
		signalBus.EmitSignal(SignalBus.SignalName.SFX, "Shell");
		QueueFree();
	}

    private void OnPitfall(Node2D body)
	{
		if(body.IsInGroup("ShellEnemy") && !IsOnFloor())
		{
			Die();
		}
	}
	public override void _ExitTree()
    {
        healthComponent.Death -= Die;
	    signalBus.PitFall -= OnPitfall;
    }
}
