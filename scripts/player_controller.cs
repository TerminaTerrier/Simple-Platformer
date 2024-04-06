using Godot;
using System;

public partial class player_controller : Node2D
{
	public Vector2 direction {get; private set;}
	
	public override void _Ready()
	{

	}

    public override void _Process(double delta)
    {
        if(Input.IsActionPressed("InputUp"))
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
		
		
    }




}
