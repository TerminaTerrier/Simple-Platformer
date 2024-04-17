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
	public Vector2 direction {get; private set;}
	public bool PressFlag {get; private set;}
	private bool IsJumping;
	private Dictionary<string, bool> actionStates = new();

    //the first jump is always the highest as gravity hasn't fully accelerated
    


    public override void _Input(InputEvent @event)
    {
        
        if(Input.IsActionJustPressed("InputUp"))
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
		
		//if(Input.IsActionPressed("InputLeft") && Input.IsActionJustPressed("InputUp") && IsJumping == false)
		//{
		//	direction = new Vector2(-1f,-1f);
	//		PressFlag = true;
	//	}

		if(Input.IsActionPressed("InputRight"))
		{
			direction = Vector2.Right;
			PressFlag = true;
		}

		//if(Input.IsActionPressed("InputRight") &&  Input.IsActionJustPressed("InputUp") && IsJumping == false)
	//	{
		//	direction = new Vector2(1,-1);
		//	PressFlag = true;
		//}
		
		
		//GD.Print(direction);
		GD.Print(PressFlag);
		//GD.Print(IsJumping);
    }

	private void Jump()
	{
		raycastComponent.SetRaycastParamaters(player.GlobalPosition, player.GlobalPosition + new Vector2(0, 12)); //consider making a more complicated formula so that the raycast parameters adapt to the size of the character body
		
		if(raycastComponent.GetRayCastQuery().Count != 0)
		{
			//velocityComponent.SetMaxSpeed(300);
			//velocityComponent.SetAccelerationRate(0.0000001f);
			velocityComponent.Velocity += new Vector2(0,-300f);
			
		    PressFlag = true;
		}
		//velocityComponent.SetAccelerationRate(0.045f);
	}



}
