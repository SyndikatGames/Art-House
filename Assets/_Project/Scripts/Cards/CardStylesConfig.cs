using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Project/Card Styles", fileName = "CardStyles")]
public class CardStylesConfig : ScriptableObject
{
    [SerializeField] private List<CardStyle> _cardStyles;


    public CardStyle GetStyle(RarityType rarityType) 
        => _cardStyles.Find(cardStyle => cardStyle.rarityType == rarityType);

}

public static partial class Configs
{
    public static CardStylesConfig CardStyles
        => Resources.Load<CardStylesConfig>("CardStyles");
}
