using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StyleData
{
    public Sprite toRightWallSprite;
    public Sprite toLeftWallSprite;
    public Sprite floorSprite;
}


[CreateAssetMenu(menuName = "Project/Styles", fileName = "Styles")]
public class StylesConfig : ScriptableObject
{
    [SerializeField] private List<StyleData> _styles;


    public StyleData GetStyle(int index) => _styles[index];
    

}

public static partial class Configs
{
    public static StylesConfig GetStyles(int roomIndex)
        => Resources.Load<StylesConfig>($"Styles/{roomIndex}");
}
