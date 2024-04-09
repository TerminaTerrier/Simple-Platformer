using Godot;
using System;

public partial class PlayerController : Node2D
{
	public Vector2 direction {get; private set;}
	
	public override void _Ready()
	{

	}

    public override void _Process(double delta)
    {

		if(Input.IsActionJustPressed("InputUp") == false || Input.IsActionPressed("InputDown") == false || Input.IsActionPressed("InputLeft") == false || Input.IsActionPressed("InputRight") == false)
		{
			direction = Vector2.Zero;
		}

        if(Input.IsActionJustPressed("InputUp"))
		{
			direction = Vector2.Up;
		}
		

		if(Input.IsActionPressed("InputDown"))
		{
			direction = Vector2.Down;
		}
		

		if(Input.IsActionPressed("InputLeft"))
		{
			direction = Vector2.Left;
		}
		

		if(Input.IsActionPressed("InputRight"))
		{
			direction = Vector2.Right;
		}
		
		GD.Print(direction);
		
    }




}
