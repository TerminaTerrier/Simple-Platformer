using Godot;
using System;

public partial class PlayerController : Node2D
{
	[Export]
	private RaycastComponent raycastComponent;
	public Vector2 direction {get; private set;}
	public bool PressFlag {get; private set;}
	private bool IsJumping;

	
	

    public override void _Process(double delta)
    {

		if(Input.IsActionPressed("InputUp") == false || Input.IsActionPressed("InputDown") == false || Input.IsActionPressed("InputLeft") == false || Input.IsActionPressed("InputRight") == false)
		{
			PressFlag = false;
		}


        if(Input.IsActionPressed("InputUp") && IsJumping == false)
		{	
			direction = Vector2.Up;
		    PressFlag = true;
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
		

		if(Input.IsActionPressed("InputRight"))
		{
			direction = Vector2.Right;
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
