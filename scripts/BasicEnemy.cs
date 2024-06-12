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
	bool directionSwitch = true;	
	SignalBus signalBus;
	StateMachine stateMachine = new();
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		signalBus.PitFall += (Node2D body) => 
		{
			if(body.IsInGroup("BasicEnemy"))
			{
				Die();
			}
		}; 

		healthComponent.Death += Die;

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

	
		velocityComponent.NormalForceCheck(this);
		
	
		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();
		//GD.Print(target);
	}

	private void Die()
	{
		 signalBus.EmitSignal(SignalBus.SignalName.SFX, "Squish");
		QueueFree();
	}
}
