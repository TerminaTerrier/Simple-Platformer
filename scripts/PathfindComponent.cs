using Godot;
using System;

public partial class PathfindComponent : Node2D
{

	[Export]
	VelocityComponent velocityComponent;
	[Export]
	public NavigationAgent2D navAgent;
	
	public void SetTargetPosition(Vector2 target)
	{
		navAgent.TargetPosition = target;
	}
	
	public void FollowPath()
	{
		if(navAgent.IsNavigationFinished())
		{
			velocityComponent.Decelerate();
		}
		
		var direction = (navAgent.GetNextPathPosition() - GlobalPosition).Normalized();
		velocityComponent.AccelerateInDirection(direction, 1f);
		navAgent.SetVelocityForced(velocityComponent.Velocity);
	}
}
