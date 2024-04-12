using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerController : Node2D
{
	[Export]
	private VelocityComponent velocityComponent;
	[Export]
	private RaycastComponent raycastComponent;
	public Vector2 direction {get; private set;}
	public bool PressFlag {get; private set;}
	private bool IsJumping;
	private Dictionary<string, bool> actionStates = new();

    //the first jump is always the highest as gravity hasn't fully accelerated
    //diagonal movement seems to jump higher


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


		if(Input.IsActionPressed("InputDown"))
		{
			direction = Vector2.Down;
			PressFlag = true;
		}
		

		if(Input.IsActionPressed("InputLeft"))
		{
			direction = Vector2.Left;
			PressFlag = true;
		}
		
		if(Input.IsActionPressed("InputLeft") && Input.IsActionJustPressed("InputUp") && IsJumping == false)
		{
			direction = new Vector2(-1f,-1f);
			PressFlag = true;
		}

		if(Input.IsActionPressed("InputRight"))
		{
			direction = Vector2.Right;
			PressFlag = true;
		}

		if(Input.IsActionPressed("InputRight") &&  Input.IsActionJustPressed("InputUp") && IsJumping == false)
		{
			direction = new Vector2(1,-1);
			PressFlag = true;
		}
		
		
		//GD.Print(direction);
		GD.Print(PressFlag);
		//GD.Print(IsJumping);
    }

	private void Jump()
	{
		 
		if(IsJumping == false)
		{
			direction = Vector2.Up;
			//velocityComponent.SetMaxSpeed(300);
			velocityComponent.SetAccelerationRate(1f);
		   PressFlag = true;
		}

		if(IsJumping == true)
		{
			velocityComponent.SetAccelerationRate(0.045f);
		}
		//velocityComponent.SetAccelerationRate(0.045f);
	}

	public void JumpCheck(Vector2 from, Vector2 to)
	{
		raycastComponent.SetRaycastParamaters(from, to);

		if(raycastComponent.GetRayCastQuery().Count != 0)
		{
			IsJumping = false;
		}
		else
		{
			IsJumping = true;
		}
		//GD.Print(raycastComponent.GetRayCastQuery() != null);
	}


}
