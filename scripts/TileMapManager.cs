using Godot;
using Godot.NativeInterop;
using System;

public partial class TileMapManager : TileMap
{
	SignalBus signalBus;
	public PackedScene token{ get; private set; } = GD.Load<PackedScene>("res://scenes/token.tscn");
	public PackedScene regPowerUp{get; private set;} = GD.Load<PackedScene>("res://scenes/power_up_regular.tscn");
	public PackedScene offPowerUp{get; private set; } = GD.Load<PackedScene>("res://scenes/power_up_offensive.tscn");
	public PackedScene invPowerUp{get; private set; } = GD.Load<PackedScene>("res://scenes/power_up_invincible.tscn");
	private Node2D powerUpInstance;
	[Export]
	int tileMapLevel;
	int currentLevelID = 1;
	int containerData;
    public override void _Ready()
    {
		//GD.Print(IsLayerNavigationEnabled(1));
		//GD.Print(GetLayerNavigationMap(1).IsValid); -- returns false for some unknown reason, which layer passed does not matter
        signalBus = GetNode<SignalBus>("/root/SignalBus");
		
		signalBus.SpecialBox += OnSpecialBoxHit;
		signalBus.BrickHit += OnBrickHit;

		signalBus.LevelComplete += (levelID) => currentLevelID = levelID;

		signalBus.Warp += (warpID, telePosition) => currentLevelID = warpID;
    }

	private void OnSpecialBoxHit(Vector2I spawnPosition, int powerUpState)
	{
		//GD.Print(spawnPosition);
	  if(tileMapLevel == currentLevelID)
	  {
		switch(currentLevelID)
		{
			case 1:
			containerData = (int)LevelData.GetLevelOneCustomData(spawnPosition + new Vector2I(0,15), "ContainerData", 2);
		    break;
			case -1:
			//containerData = (int)SubLevelOneData.GetSubLevelOneCustomData(spawnPosition + new Vector2I(0,15), "ContainerData", 2);
			break;
		}
		
		if(currentLevelID == 1 || currentLevelID == 2 || currentLevelID == 3)
		{

			switch (containerData)
			{
				case 0:
				break;

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


		AddChild(powerUpInstance);
		powerUpInstance.Position = spawnPosition;
		}
	  }
		//GD.Print("Emitted");

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
