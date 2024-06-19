using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class BasicEnemy : CharacterBody2D
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
	Sprite2D sprite;
	bool directionSwitch = true;	
	SignalBus signalBus;
	StateMachine stateMachine = new();
	
    public override void _EnterTree()
    {
        signalBus = GetNode<SignalBus>("/root/SignalBus");
		signalBus.PitFall += OnPitfall;

		healthComponent.Death += Die;
    }
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

		velocityComponent.NormalForceCheck(this);
		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();
	}
	private void OnPitfall(Node2D body)
	{
		//It would probably be better if the entity detected the fall zone rather than vice versa.
	    if(body.IsInGroup("BasicEnemy") && !IsOnFloor())
		{ 
			Die();
		}
	}
	private void Die()
	{
	    signalBus.EmitSignal(SignalBus.SignalName.SFX, "Squish");
		QueueFree();
	}

	public override void _ExitTree()
    {
        healthComponent.Death -= Die;
	    signalBus.PitFall -= OnPitfall;
    }
}
