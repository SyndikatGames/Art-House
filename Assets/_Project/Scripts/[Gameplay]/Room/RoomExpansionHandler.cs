using System.Collections.Generic;
using UnityEngine;
using VG2;

public class RoomExpansionHandler
{
    private Item _rootItem;

    private const int cellsPerTile = 8;
    private const int cellsHeight = 27;


    public RoomExpansionHandler(Item rootItem) 
    {
        _rootItem = rootItem;
    }


    public void UpdateRoomSize()
    {
        var size = GameState.CurrentRoom.size.Value;
        var placeGridDataList = new List<PlaceGridData>();

        placeGridDataList.Add(new PlaceGridData
        {
            gridType = GridType.Floor,
            offset = Vector3Int.zero,
            size = size * cellsPerTile
        });

        placeGridDataList.Add(new PlaceGridData
        {
            gridType = GridType.ToLeft,
            offset = Vector3Int.zero,
            size = new Vector2Int(size.x * cellsPerTile, cellsHeight)
        });

        placeGridDataList.Add(new PlaceGridData
        {
            gridType = GridType.ToRight,
            offset = Vector3Int.zero,
            size = new Vector2Int(size.x * cellsPerTile, cellsHeight)
        });

        _rootItem.SetPlaceGridData(placeGridDataList);
    }


}
