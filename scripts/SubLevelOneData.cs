using Godot;
using System;

public partial class SubLevelOneData : Node
{
	static TileMap sub_lvl1_tileMap;
	SignalBus signalBus;
	public override void _Ready()
	{
		sub_lvl1_tileMap = GetNode<TileMap>("TileMap");
		signalBus = GetNode<SignalBus>("/root/SignalBus");

	}
	public static TileData GetSubLevelOneTileData(Vector2 position, int layer)
	{
		var local_position = sub_lvl1_tileMap.LocalToMap(position);
		return sub_lvl1_tileMap.GetCellTileData(layer, local_position);
	}

	public static Variant GetSubLevelOneCustomData(Vector2 position, string dataName, int layer)
	{
		var data = GetSubLevelOneTileData(position, layer);
		return data.GetCustomData(dataName);
	}

	public static Rid GetNavigationMap(int layer)
	{
		return sub_lvl1_tileMap.GetLayerNavigationMap(layer);
	}

}
