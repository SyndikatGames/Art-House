using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Project/Design", fileName = "Design")]
public class DesignConfig : ScriptableObject
{
    [SerializeField] private List<RarityStyle> _rarityStyles;

    public RarityStyle GetRarityStyle(RarityType rarityType) 
        => _rarityStyles.Find(cardStyle => cardStyle.rarityType == rarityType);

}

public static partial class Configs
{
    public static DesignConfig CardStyles
        => Resources.Load<DesignConfig>("Design");
}
