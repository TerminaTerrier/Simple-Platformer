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
	

    public override void _Input(InputEvent @event)
    {
        if(@event.IsActionPressed("InputUp"))
		{	
			Jump();
		}
    }
    public override void _Process(double delta)
    {

		if(Input.IsActionJustPressed("InputUp") == false || Input.IsActionPressed("InputDown") == false || Input.IsActionPressed("InputLeft") == false || Input.IsActionPressed("InputRight") == false)
		{
			PressFlag = false;
		}
		

		if(Input.IsActionPressed("InputLeft"))
		{
			direction = Vector2.Left;
			PressFlag = true;
		}
		

		if(Input.IsActionPressed("InputRight"))
		{
			direction = Vector2.Right;
			PressFlag = true;
		}

	
    }
	
	private void Jump()
	{
		raycastComponent.SetRaycastParamaters(player.GlobalPosition, player.GlobalPosition + new Vector2(0, 12)); //consider making a more complicated formula so that the raycast parameters adapt to the size of the character body
		
		if(raycastComponent.GetRayCastQuery().Count != 0)
		{
			PressFlag = true;
			velocityComponent.AccelerateInDirection(Vector2.Up, jumpStrength * 100);
		}
	}



}
