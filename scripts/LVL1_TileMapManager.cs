using Godot;
using System;

public partial class LVL1_TileMapManager : TileMap
{
	SignalBus signalBus;
	public PackedScene powerUp{get; private set;} = GD.Load<PackedScene>("res://scenes/power_up_placeholder.tscn");
    public override void _Ready()
    {
		//GD.Print(IsLayerNavigationEnabled(1));
		//GD.Print(GetLayerNavigationMap(1).IsValid); -- returns false for some unknown reason, which layer passed does not matter
        signalBus = GetNode<SignalBus>("/root/SignalBus");
		
		signalBus.SpecialBox += OnSpecialBoxHit;
    }

	private void OnSpecialBoxHit(Vector2I spawnPosition)
	{
		GD.Print(spawnPosition);
		Node2D powerUpInstance = (Node2D)powerUp.Instantiate();
		AddChild(powerUpInstance);
		powerUpInstance.Position = spawnPosition;
	}
}
