using Godot;
using System;
using System.Net.NetworkInformation;

public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void PlayerDeathEventHandler();
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	PlayerController playerController;
	[Export]
	CollisionHandler collisionHandler;
	[Export]
	HealthComponent healthComponent;
	[Export]
	private int lives = 3;
	SignalBus signalBus;
	StateMachine stateMachine = new();
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		healthComponent.OnDeath += Die;

		stateMachine.AddState(IdleState);
		stateMachine.Enter();
	}

    public override void _PhysicsProcess(double delta)
    {
        stateMachine.Update();
		
    }

	private void IdleState()
	{
		if(GetSlideCollisionCount() != 0)
		{	
			//if(collisionHandler.CheckCollisionObjectType(collisionHandler.GetCollisionObject(GetLastSlideCollision()), typeof(TileMap)))
			//{
			//	velocityComponent.NormalForceCheck(collisionHandler.GetCollisionObject(GetLastSlideCollision()), collisionHandler.GetCollisionPosition(GetLastSlideCollision()), collisionHandler.GetCollisionAngle(GetLastSlideCollision()));
			//}
			//velocityComponent.CollisionCheck(GetLastSlideCollision());
			velocityComponent.NormalForceCheck(this);
			//GD.Print(GetLastSlideCollision().GetNormal());
		}
		
		if(playerController.PressFlag == false)
		{
			velocityComponent.Decelerate();
		}
		else
		{
			stateMachine.AddState(WalkState);
			stateMachine.Enter();
		}

		velocityComponent.Move(this);
		//gravity must be called last to avoid it being added to velocity before a normal force check is completed
		velocityComponent.ApplyGravity();
		//GD.Print(velocityComponent.GetVelocity());
		
	}
	private void WalkState()
	{
		if(GetSlideCollisionCount() != 0)
		{
			velocityComponent.NormalForceCheck(this);
			//GD.Print(GetLastSlideCollision().GetNormal());
		}
	
		if(playerController.PressFlag == false)
		{
			stateMachine.AddState(IdleState);
			stateMachine.Enter();
		}
		else if(playerController.PressFlag == true)
		{
			velocityComponent.AccelerateInDirection(playerController.direction, 40f);
		}

		if(GetSlideCollisionCount() != 0)
		{
		var collisionData = GetLastSlideCollision();
		GD.Print(collisionData.GetNormal());
		if(collisionData.GetNormal() == Vector2.Down)
		{
			if(collisionHandler.CheckCollisionObjectType(collisionHandler.GetCollisionObject(collisionData), typeof(TileMap)))
			{
				GodotObject collider = collisionHandler.GetCollisionObject(collisionData);
				var tileMap = (TileMap)collider;
				//var tileMap = collisionHandler.CastCollisionObject<TileMap>(collider); -- invalidcastexception error, look into fixing
				GD.Print(tileMap.GetCellAtlasCoords(1, tileMap.LocalToMap(collisionData.GetPosition() - new Vector2(0, 10))));
				if(tileMap.GetCellAtlasCoords(1, tileMap.LocalToMap(collisionData.GetPosition() - new Vector2(0, 10))) == new Vector2I(1,0))
				{
					signalBus.EmitSignal(SignalBus.SignalName.SpecialBox, tileMap.MapToLocal(tileMap.LocalToMap(collisionData.GetPosition()- new Vector2I(0,25))));
					GD.Print("Emitted");
				}
			}
		}
		}
		
		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();
		//gravity not appearing? GD.Print(Velocity);
		//GD.Print(velocityComponent.GetVelocity());
		
	}

	private void Die()
	{
		EmitSignal("PlayerDeath");
		lives--;
		//GD.Print(lives);
		if(lives == 0)
		{
			signalBus.EmitSignal("GameOver");
		}
		GlobalPosition = Vector2.Zero;
	}
}