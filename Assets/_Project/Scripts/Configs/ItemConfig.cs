using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Project/Item", fileName = "Item")]
public class ItemConfig : ScriptableObject
{
    [System.Serializable]
    private struct RaritySprite
    {
        public RarityType rarityType;
        public Sprite sprite;
    }

    [field: SerializeField] public Item ItemPrefab { get; private set; }
    [SerializeField] private List<RaritySprite> _sprites;


    public Sprite GetSprite(RarityType rarityType)
        => _sprites.Find((raritySprite) => raritySprite.rarityType == rarityType).sprite;


}

public static partial class Configs
{

    private static ItemConfig[] _allItems;
    public static ItemConfig[] GetAllItems()
    {
        if (_allItems == null)
            _allItems = Resources.LoadAll<ItemConfig>("Items");

        return _allItems;
    }
        


    public static ItemConfig GetItem(ItemType itemType) =>
        Resources.Load<ItemConfig>($"Items/{itemType}");


}

