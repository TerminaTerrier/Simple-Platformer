using Godot;
using System;

public partial class LVL1_TileMapManager : TileMap
{
	SignalBus signalBus;
	public PackedScene token{ get; private set; } = GD.Load<PackedScene>("res://scenes/token.tscn");
	public PackedScene regPowerUp{get; private set;} = GD.Load<PackedScene>("res://scenes/power_up_regular.tscn");
	public PackedScene offPowerUp{get; private set; } = GD.Load<PackedScene>("res://scenes/power_up_offensive.tscn");
	public PackedScene invPowerUp{get; private set; } = GD.Load<PackedScene>("res://scenes/power_up_invincible.tscn");
	private Node2D powerUpInstance;
    public override void _Ready()
    {
		//GD.Print(IsLayerNavigationEnabled(1));
		//GD.Print(GetLayerNavigationMap(1).IsValid); -- returns false for some unknown reason, which layer passed does not matter
        signalBus = GetNode<SignalBus>("/root/SignalBus");
		
		signalBus.SpecialBox += OnSpecialBoxHit;
		signalBus.BrickHit += OnBrickHit;
    }

	private void OnSpecialBoxHit(Vector2I spawnPosition, int powerUpState)
	{
		//GD.Print(spawnPosition);
		int containerData = (int)LevelOneData.GetLevelOneCustomData(spawnPosition + new Vector2I(0,15), "ContainerData", 2);
		//GD.Print(containerData);
		switch (containerData)
		{
			

			case 1:
			powerUpInstance = (Node2D)token.Instantiate();
			break;
			case 2:
			if(powerUpState == 0)
			{
			 powerUpInstance = (Node2D)regPowerUp.Instantiate();
			}
			else if(powerUpState == 1)
			{
			 powerUpInstance = (Node2D)offPowerUp.Instantiate();
			}
			break;
			case 3:
			powerUpInstance = (Node2D)invPowerUp.Instantiate();
			break;
		}
		
		signalBus.EmitSignal(SignalBus.SignalName.SFX, "Special_Block");
		EraseCell(2, LocalToMap(spawnPosition + new Vector2I(0,10)));
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
		signalBus.EmitSignal(SignalBus.SignalName.SFX, "Brick");
		//GD.Print("OnBrickHit: " + tilePosition);
		//GD.Print("emitted");
	}
}
