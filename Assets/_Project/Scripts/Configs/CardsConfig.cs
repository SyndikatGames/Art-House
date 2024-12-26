using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Project/Cards", fileName = "Cards")]
public class CardsConfig : ScriptableObject
{
    [field: SerializeField] public InventoryCard InventoryCardPrefab { get; private set; }
    [field: SerializeField] public UnboxedCardView UnboxedCardPrefab { get; private set; }


    [SerializeField] private List<CardStyle> _rarityStyles;

    public CardStyle GetRarityStyle(RarityType rarityType) 
        => _rarityStyles.Find(cardStyle => cardStyle.rarityType == rarityType);

}
