using Godot;
using System;

public partial class LevelData : Node
{
	static TileMap lvl1_tileMap;
	SignalBus signalBus;

    public override void _EnterTree()
    {
      lvl1_tileMap =  GetNode<TileMap>("TileMap");
	 // GD.Print("Enter");
    }
    public override void _Ready()
	{
		
		signalBus = GetNode<SignalBus>("/root/SignalBus");

		//signalBus.Warp += (warpVal, telePosition) => GD.Print("IMPORTANT: " + lvl1_tileMap == null);

	}
	public static TileData GetLevelOneTileData(Vector2 position, int layer)
	{
		var local_position = lvl1_tileMap.LocalToMap(position);
		return lvl1_tileMap.GetCellTileData(layer, local_position);
	}

	public static Variant GetLevelOneCustomData(Vector2 position, string dataName, int layer)
	{
		
		var data = GetLevelOneTileData(position, layer);
		GD.Print(data);
		GD.Print(dataName);
		return data.GetCustomData(dataName);
	}

	public static Rid GetNavigationMap(int layer)
	{
		return lvl1_tileMap.GetLayerNavigationMap(layer);
	}

	
}
