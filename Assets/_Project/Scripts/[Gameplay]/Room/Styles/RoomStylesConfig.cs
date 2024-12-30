using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RoomStyleData
{
    public Sprite toRightWallSprite;
    public Sprite toLeftWallSprite;
    public Sprite floorSprite;
}


[CreateAssetMenu(menuName = "Project/Styles", fileName = "Styles")]
public class RoomStylesConfig : ScriptableObject
{
    [field: SerializeField] public RoomStylesScreenView RoomStylesScreenPrefab { get; private set; }


    [SerializeField] private List<RoomStyleData> _styles;


    public RoomStyleData GetStyle(int index) => _styles[index];
    

}

