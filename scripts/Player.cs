using Godot;
using System;


public partial class Player : CharacterBody2D
{
	
	[Export]
	VelocityComponent velocityComponent;
	[Export]
	PlayerController playerController;
	[Export]
	CollisionHandler collisionHandler;
	[Export]
	HealthComponent healthComponent;
	[Export]
	HurtboxComponent hurtboxComponent;
	[Export]
	HitboxComponent hitboxComponent;
	[Export]
	private int lives = 3;
	[Export]
	CollisionShape2D collisionShape2D;
	[Export]
	Sprite2D sprite;
	bool spawnLock = false;	
	int powerUpState = 0;
	SignalBus signalBus;
	StateMachine stateMachine = new();
	public override void _Ready()
	{
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		healthComponent.Death += Die;
		healthComponent.Damage += OnDamage;
		signalBus.PowerUp += ModifyPowerUpState;
		signalBus.GlobalTimeout += Die;
		signalBus.Warp += OnWarp;
		signalBus.PitFall += OnPitfall;

		signalBus.CounterRollover += OnCounterRollover;

		stateMachine.AddState(IdleState);
		stateMachine.Enter();
	}

    public override void _PhysicsProcess(double delta)
    {
        stateMachine.Update();
		
    }

    public void ModifyPowerUpState(int ID)
	{
		switch (ID)
		{
			case 0:
			healthComponent.SetHealth(1);
			collisionShape2D.Scale = new Vector2(1,1f);
			hurtboxComponent.Scale = new Vector2(1.01f,1.01f);
			hitboxComponent.Scale = new Vector2(1,1f);
			sprite.Scale = new Vector2(0.875f,1.188f);
			break;
			case 1:
			if(powerUpState != 1)
			{
			healthComponent.SetHealth(2);
			collisionShape2D.Scale = new Vector2(1,1.5f);
			hurtboxComponent.Scale = new Vector2(1.01f,1.01f);
			hitboxComponent.Scale = new Vector2(1,1.25f);
			sprite.Scale = new Vector2(1.25f,1.875f);
			powerUpState = 1;
			}
			break;
			case 2:
			if(powerUpState != 2)
			{
				healthComponent.SetHealth(2);
				collisionShape2D.Scale = new Vector2(1,1.5f);
				hurtboxComponent.Scale = new Vector2(1.01f,1.01f);
				hitboxComponent.Scale = new Vector2(1,1.25f);
				sprite.Scale = new Vector2(1.25f,1.875f);
				playerController.offensiveState = true;
				powerUpState = 2;
			}
			break;
			case 3:
				if(powerUpState != 3)
				{	
					Timer timer = new();
					AddChild(timer);
					timer.Start(15);
					hurtboxComponent.Monitoring = false;
					hitboxComponent.SetCollisionLayerValue(5, true);
					velocityComponent.SetMaxSpeed(250f, 200f);
						if(powerUpState == 0)
						{
							hitboxComponent.Scale = new Vector2(1.2f,1f);
							timer.Timeout += () => {hurtboxComponent.Monitoring = true; hitboxComponent.Scale = new Vector2(1f,1f); hitboxComponent.SetCollisionLayerValue(5, false); velocityComponent.SetMaxSpeed(100f, 200f); powerUpState = 0; };
						}
						else if(powerUpState == 1 || powerUpState == 2)
						{
							hitboxComponent.Scale = new Vector2(1.2f,1.25f);	
							timer.Timeout += () => {hurtboxComponent.Monitoring = true; hitboxComponent.Scale = new Vector2(1f,1.25f); hitboxComponent.SetCollisionLayerValue(5, false); velocityComponent.SetMaxSpeed(100f, 200f); powerUpState = 1; };
						}
				}
			break;
		}

	}
	
	private void IdleState()
	{
		
			velocityComponent.NormalForceCheck(this);
			
		
		if(playerController.PressFlag == false)
		{
			velocityComponent.Decelerate();
		}
		else
		{
			stateMachine.AddState(WalkState);
			stateMachine.Enter();
		}
		spawnLock = false;
		velocityComponent.Move(this);
		//gravity must be called last to avoid it being added to velocity before a normal force check is completed
		velocityComponent.ApplyGravity();
	//	GD.Print(velocityComponent.GetVelocity());
		//GD.Print(IsOnFloor());
	//	GD.Print(GetSlideCollisionCount());
	}
	private void WalkState()
	{
		
			velocityComponent.NormalForceCheck(this);
			//GD.Print(GetLastSlideCollision().GetNormal());
		
	
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
		//GD.Print(collisionData.GetNormal());
		if(collisionData.GetNormal() == Vector2.Down)
		  {
			if(collisionHandler.CheckCollisionObjectType(collisionHandler.GetCollisionObject(collisionData), typeof(TileMap)))
			{
				
				GodotObject collider = collisionHandler.GetCollisionObject(collisionData);
				var tileMap = (TileMap)collider;
				//var tileMap = collisionHandler.CastCollisionObject<TileMap>(collider); -- invalidcastexception error, look into fixing
				GD.Print(tileMap.GetCellAtlasCoords(1, tileMap.LocalToMap(collisionData.GetPosition() - new Vector2(0, 10))));
				if(tileMap.GetCellAtlasCoords(1, tileMap.LocalToMap(collisionData.GetPosition() - new Vector2(0, 10))) == new Vector2I(1,0) && spawnLock == false)
				{
				    signalBus.EmitSignal(SignalBus.SignalName.SpecialBox, tileMap.MapToLocal(tileMap.LocalToMap(collisionData.GetPosition()- new Vector2I(0,25))), powerUpState);
					//GD.Print("Emitted");
					spawnLock = true;
				}

			    
			   if(powerUpState == 1 || powerUpState == 2)
			   {
			     if(tileMap.GetCellAtlasCoords(1, tileMap.LocalToMap(collisionData.GetPosition() - new Vector2(0, 10))) == new Vector2I(2,0) || tileMap.GetCellAtlasCoords(1, tileMap.LocalToMap(collisionData.GetPosition() - new Vector2(0, 10))) == new Vector2I(2,1))
				 {
			     signalBus.EmitSignal(SignalBus.SignalName.BrickHit, collisionData.GetPosition());
				// GD.Print(tileMap.GetCellAtlasCoords(1, tileMap.LocalToMap(collisionData.GetPosition() - new Vector2(0, 10))));
					//GD.Print("emited");
				 }

			    
			   }
			}
			}
		  }
		
	
		
		//GD.Print(GetSlideCollisionCount());
	//	GD.Print(IsOnFloor());
		velocityComponent.Move(this);
		velocityComponent.ApplyGravity();
		//gravity not appearing? GD.Print(Velocity);
		//GD.Print(velocityComponent.GetVelocity());
		
	}

	private void OnDamage()
	{
		hurtboxComponent.SetDeferred("monitoring", false);
		powerUpState = 0;
		ModifyPowerUpState(0);
		Timer timer = new();
		AddChild(timer);
		timer.OneShot = true;
		timer.Start(2);
		timer.Timeout += () => hurtboxComponent.SetDeferred("monitoring", true);

	}
	private void Die()
	{
		
		lives--;
		//GD.Print(lives);
		if(lives < 0)
		{
			signalBus.EmitSignal("GameOver");
		}
		GlobalPosition = new Vector2(0, -5);
		signalBus.EmitSignal(SignalBus.SignalName.LivesUpdate, lives);
	}

	private void OnPitfall(Node2D body)
	{
		if(body.IsInGroup("Player"))
			{
				Die();
				powerUpState = 0;
				ModifyPowerUpState(0);
			}
	}

	private void OnWarp(int warpVal, Vector2 telePosition)
	{
		SetCollisionLayerValue(2, false); 
		Timer timer = new();
		AddChild(timer);
		timer.Start(1);
		timer.Timeout += () =>  SetCollisionLayerValue(2, true); 
	}

	private void OnCounterRollover()
	{
		lives++;
		signalBus.EmitSignal(SignalBus.SignalName.LivesUpdate, lives);
	}
	public override void _ExitTree()
	{
		healthComponent.Death -= Die;
		healthComponent.Damage -= OnDamage;

		signalBus.PowerUp -= ModifyPowerUpState;
		signalBus.GlobalTimeout -= Die;
		signalBus.PitFall -= OnPitfall;
		signalBus.Warp -= OnWarp;
		signalBus.CounterRollover -= OnCounterRollover;
	}
}