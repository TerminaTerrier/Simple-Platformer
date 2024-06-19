using Godot;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

public partial class CollisionHandler : Node2D
{
	public GodotObject GetCollisionObject(KinematicCollision2D collisionData)
	{
		return collisionData.GetCollider();
	}

	public bool CheckCollisionObjectType(GodotObject collisionObject, Type type)
	{
		Console.WriteLine(type.Name);

		if(type.IsInstanceOfType(collisionObject))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public T CastCollisionObject<T>(GodotObject collisionObject)
	{
		return (T)Convert.ChangeType(collisionObject, typeof(T));
	}

	public float GetCollisionAngle(KinematicCollision2D collisionData)
	{
		return Mathf.Round(collisionData.GetAngle());
	}

	public Vector2 GetCollisionPosition(KinematicCollision2D collisionData)
	{
		return collisionData.GetPosition();
	}
}
