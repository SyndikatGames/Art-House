using System.Collections.Generic;
using UnityEngine;
using VG;

public class RoomExpansion : MonoBehaviour
{
    [SerializeField] private Item _room;

    private const int cellsPerTile = 8;
    private const int cellsHeight = 27;


    private void OnEnable()
    {
        Saves.String[Key_Save.room_size_data(0)].onChanged += UpdateRoomSize;
    }

    private void OnDisable()
    {
        Saves.String[Key_Save.room_size_data(0)].onChanged -= UpdateRoomSize;
    }


    public void UpdateRoomSize()
    {
        var size = Saves.RoomSize;
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

        _room.SetPlaceGridData(placeGridDataList);
    }


}
