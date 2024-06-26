using Godot;
using System;
using System.Collections.Generic;


public partial class PlayerController : Node2D
{
	[Export]
	private CharacterBody2D player;
	[Export]
	private VelocityComponent velocityComponent;
	[Export]
	private RaycastComponent raycastComponent;
	[Export]
	private int jumpStrength = 200;
	public Vector2 direction {get; private set;}
	public bool PressFlag {get; private set;}
	bool warpFlag;
	int warpValue;
	public bool offensiveState {get; set;}
	Vector2 teleportPosition;
	SignalBus signalBus;
	

    public override void _Ready()
    {
        signalBus = GetNode<SignalBus>("/root/SignalBus");
		signalBus.WarpZoneEnter += (int warpVal, Vector2 telePosition) => {warpFlag = true; warpValue = warpVal; teleportPosition = telePosition; GD.Print(warpValue); };
		signalBus.WarpZoneExit += () => warpFlag = false;
    }

    public override void _Input(InputEvent @event)
    {
        if(@event.IsActionPressed("InputUp"))
		{	
			Jump();
		}

		if(offensiveState == true && player.IsOnFloor() == false)
		{
			if(@event.IsActionPressed("InputUp"))
			{
				SpecialAction();
			}
		}

		if(warpFlag == true)
		{
			if(@event.IsActionPressed("InputDown"))
			{
				signalBus.EmitSignal(SignalBus.SignalName.Warp, warpValue, teleportPosition);
				signalBus.EmitSignal(SignalBus.SignalName.SFX, "Warp");
			}
		}
    }
    public override void _Process(double delta)
    {
		if(Input.IsActionJustReleased("InputDown") || Input.IsActionJustReleased("InputLeft") || Input.IsActionJustReleased("InputRight"))
		{
			PressFlag = false;
		}

		if(Input.IsActionPressed("InputLeft"))
		{
			direction = Vector2.Left;
			PressFlag = true;
			//GD.Print("left");
		}
		
		if(Input.IsActionPressed("InputRight"))
		{
			direction = Vector2.Right;
			PressFlag = true;

		}
    }
	
	private void Jump()
	{
		//Consider making a more complicated formula so that the raycast parameters adapt to the size of the character body.
		raycastComponent.SetRaycastParamaters(player.GlobalPosition, player.GlobalPosition + new Vector2(7, 15)); 
		var rcQuery1 = raycastComponent.GetRayCastQuery();

		raycastComponent.SetRaycastParamaters(player.GlobalPosition, player.GlobalPosition + new Vector2(-7, 15));
		var rcQuery2 = raycastComponent.GetRayCastQuery();

		if(rcQuery1.Count != 0 | rcQuery2.Count != 0)
		{
			PressFlag = true;
			velocityComponent.AccelerateInDirection(Vector2.Up, jumpStrength * 100);
			signalBus.EmitSignal(SignalBus.SignalName.SFX, "Jump");
		}
		PressFlag = false;
	}

	private void SpecialAction()
	{
		signalBus.EmitSignal(SignalBus.SignalName.SpecialAction, player.GlobalPosition);
	}

}
