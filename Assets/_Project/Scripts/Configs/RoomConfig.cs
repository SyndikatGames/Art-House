using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct RoomStyleData
{
    public Sprite toRightWallSprite;
    public Sprite toLeftWallSprite;
    public Sprite floorSprite;
}


[CreateAssetMenu(menuName = "Project/Room", fileName = "Room")]
public class RoomConfig : ScriptableObject
{
    [field: SerializeField] public RoomStylesScreenView RoomStylesScreenPrefab { get; private set; }
    [field: SerializeField] public RoomExpansionWindowView RoomExpansionWindowPrefab { get; private set; }
    [field: SerializeField] public LevelUpRoomWindowView LevelUpRoomWindowPrefab { get; private set; }


    [SerializeField] private List<RoomStyleData> _styles;
    [SerializeField] private List<Vector2Int> _roomLevelSizes;


    public Vector2Int GetRoomSize(int expansionLevel) => _roomLevelSizes[expansionLevel - 1];
    public RoomStyleData GetStyle(int index) => _styles[index];


}
