using Godot;
using System;

public partial class PlayerController : Node2D
{
	[Export]
	private VelocityComponent velocityComponent;
	[Export]
	private RaycastComponent raycastComponent;
	[Export]
	private float JumpStrength = 5f;
	[Export]
	private float MoveSpeed = 0.045f;
	public Vector2 direction {get; private set;}
	public bool PressFlag {get; private set;}
	private bool IsJumping;

     //the first jump is always the highest as gravity hasn't fully accelerated
	 //diagonal movement seems to jump higher

    public override void _Process(double delta)
    {

		if(Input.IsActionJustPressed("InputUp") == false || Input.IsActionPressed("InputDown") == false || Input.IsActionPressed("InputLeft") == false || Input.IsActionPressed("InputRight") == false)
		{
			PressFlag = false;
		}

        if(Input.IsActionJustPressed("InputUp") && IsJumping == false)
		{	
			direction = Vector2.Up;
			velocityComponent.BurstAccelerate(new Vector2(0, -JumpStrength));
		    PressFlag = true;
		}

		if(Input.IsActionPressed("InputDown"))
		{
			direction = Vector2.Down;
			velocityComponent.SetAccelerationWeight(MoveSpeed);
			PressFlag = true;
		}
		

		if(Input.IsActionPressed("InputLeft"))
		{
			direction = Vector2.Left;
			velocityComponent.SetAccelerationWeight(MoveSpeed);
			PressFlag = true;
		}
		
		if(Input.IsActionPressed("InputLeft") && Input.IsActionJustPressed("InputUp") && IsJumping == false)
		{
			direction = new Vector2(-1f,-1f);
			velocityComponent.BurstAccelerate(new Vector2(0, -JumpStrength));
			velocityComponent.SetAccelerationWeight(MoveSpeed);
			PressFlag = true;
		}

		if(Input.IsActionPressed("InputRight"))
		{
			direction = Vector2.Right;
			velocityComponent.SetAccelerationWeight(MoveSpeed);
			PressFlag = true;
		}

		if(Input.IsActionPressed("InputRight") &&  Input.IsActionJustPressed("InputUp") && IsJumping == false)
		{
			direction = new Vector2(1,-1);
			velocityComponent.BurstAccelerate(new Vector2(0, -JumpStrength));
			velocityComponent.SetAccelerationWeight(MoveSpeed);
			PressFlag = true;
		}
		
		
		//GD.Print(direction);
		//GD.Print(PressFlag);
		//GD.Print(IsJumping);
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
		GD.Print(raycastComponent.GetRayCastQuery() != null);
	}


}
