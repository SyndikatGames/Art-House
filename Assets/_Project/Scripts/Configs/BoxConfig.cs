using UnityEngine;


[CreateAssetMenu(menuName = "Project/Box", fileName = "Box")]
public class BoxConfig : ScriptableObject
{



}

public static partial class Configs
{
    public static BoxConfig GetBox(RarityType rarityType) =>
        Resources.Load<BoxConfig>($"Boxes/{rarityType}");

}
