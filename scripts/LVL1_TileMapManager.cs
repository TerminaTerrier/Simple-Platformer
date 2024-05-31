using Godot;
using System;

public partial class LVL1_TileMapManager : TileMap
{
	SignalBus signalBus;
	public PackedScene powerUp{get; private set;} = GD.Load<PackedScene>("res://scenes/power_up_regular.tscn");
    public override void _Ready()
    {
		//GD.Print(IsLayerNavigationEnabled(1));
		//GD.Print(GetLayerNavigationMap(1).IsValid); -- returns false for some unknown reason, which layer passed does not matter
        signalBus = GetNode<SignalBus>("/root/SignalBus");
		
		signalBus.SpecialBox += OnSpecialBoxHit;
		signalBus.BrickHit += OnBrickHit;
    }

	private void OnSpecialBoxHit(Vector2I spawnPosition)
	{
		//GD.Print(spawnPosition);
		Node2D powerUpInstance = (Node2D)powerUp.Instantiate();

		EraseCell(1, LocalToMap(spawnPosition + new Vector2I(0,10)));
		SetCell(1, LocalToMap(spawnPosition + new Vector2I(0,10)), 3, new Vector2I(1, 1));

		//GD.Print("Emitted");

		AddChild(powerUpInstance);
		powerUpInstance.Position = spawnPosition;
	}

	private void OnBrickHit(Vector2 position)
	{
		var tilePosition = LocalToMap(position - new Vector2I(0,10));
		EraseCell(1, tilePosition);
		//GD.Print("OnBrickHit: " + tilePosition);
		GD.Print("emited");
	}
}
