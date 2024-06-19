using Godot;
using System;

public partial class RegularPowerUp : CharacterBody2D
{
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	PathfindComponent pathfindComponent;
	[Export]
	CollisionHandler collisionHandler;
	[Export]
	RaycastComponent raycastComponent;
	bool directionSwitch = true;
	bool startMoving = true;
	StateMachine stateMachine = new();
	SignalBus signalBus;
	public override void _Ready()
	{
		GD.Print("spawned");
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		stateMachine.AddState(MoveState);
		stateMachine.Enter();
	}


	public override void _PhysicsProcess(double delta) 
	{
		stateMachine.Update();
	}

	private void MoveState()
	{
		if(startMoving == true)
		{
			velocityComponent.SetVelocity(new Vector2(75, 75));
			startMoving = false;
		}


		if(GetLastSlideCollision() != null)
		{
		 	var collisionAngle = collisionHandler.GetCollisionAngle(GetLastSlideCollision());
		 
	     	if(directionSwitch == false)
		 	{
				raycastComponent.SetRaycastParamaters(GlobalPosition, GlobalPosition + new Vector2(-12, 0));

				if(raycastComponent.GetRayCastQuery().Count != 0)
				{
		    		directionSwitch = true;
					velocityComponent.SetVelocity(new Vector2(75, 50));
					GD.Print("moving");
				}
		 	}
		 	
			if(directionSwitch == true)
		 	{
				raycastComponent.SetRaycastParamaters(GlobalPosition, GlobalPosition + new Vector2(12, 0));

				if(raycastComponent.GetRayCastQuery().Count != 0)
				{
		    		directionSwitch = false;
					velocityComponent.SetVelocity(new Vector2(-75, 50));
				}
		 	}
		}

		velocityComponent.Move(this);
	}
		
	public void OnBodyEntered(Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
			signalBus.EmitSignal(SignalBus.SignalName.PowerUp, 1);
			signalBus.EmitSignal(SignalBus.SignalName.SFX, "PU-R");
			QueueFree();
		}
	}
}
