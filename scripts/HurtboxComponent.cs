using Godot;
using System;

public partial class HurtboxComponent : Area2D
{
	[Signal]
	public delegate void HitByHitboxEventHandler(HitboxComponent hitboxComponent);
	
	public override void _Ready()
	{
		AreaEntered += OnArea2DEntered;
	}
    public void OnArea2DEntered(Area2D otherArea)
	{
		if(otherArea is HitboxComponent hitboxComponent)
		{
			EmitSignal("HitByHitbox", hitboxComponent);
			GD.Print("Hit!");
		}
	}
}
