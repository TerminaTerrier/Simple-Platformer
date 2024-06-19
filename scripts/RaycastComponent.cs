using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;

public partial class RaycastComponent : Node2D
{
	public Vector2 raycastFrom {get; private set;}
	public Vector2 raycastTo {get; private set;}
	
	public Dictionary GetRayCastQuery()
	{
		var spaceState = GetWorld2D().DirectSpaceState;
    	var query = PhysicsRayQueryParameters2D.Create(raycastFrom, raycastTo);
    	var result = spaceState.IntersectRay(query);
		
		return result;
	}

    public void SetRaycastParamaters(Vector2 from, Vector2 to)
	{
		raycastFrom = from;
		raycastTo = to;
	}
}
