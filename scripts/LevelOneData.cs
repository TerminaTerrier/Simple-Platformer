using Godot;
using System;

public partial class LevelOneData : Node
{
	static TileMap lvl1_tileMap;
	public override void _Ready()
	{
		lvl1_tileMap = GetNode<TileMap>("TileMap");
	}
	public static TileData GetLevelOneTileData(Vector2 position)
	{
		var local_position = lvl1_tileMap.LocalToMap(position);
		return lvl1_tileMap.GetCellTileData(0, local_position);
	}

	public static Variant GetLevelOneCustomData(Vector2 position, String dataName)
	{
		var data = GetLevelOneTileData(position);
		return data.GetCustomData(dataName);
	}

	public static Rid GetNavigationMap(int layer)
	{
		return lvl1_tileMap.GetLayerNavigationMap(layer);
	}
}
