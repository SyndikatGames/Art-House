using System.Collections.Generic;
using UnityEngine;
using VG;

[CreateAssetMenu(menuName = "Project/Shop", fileName = "Shop")]
public class ShopConfig : ScriptableObject
{
    [System.Serializable]
    private struct RarityPrice
    {
        public RarityType rarityType;
        public float price;
    }

    [System.Serializable]
    private struct BoxPrice
    {
        public BoxType boxType;
        public float price;
    }

    [field: SerializeField] public ShopView ShopViewPrefab { get; private set; }
    [field: SerializeField] public BoxWindowView BoxWindowViewPrefab { get; private set; }



    [SerializeField] private List<RarityPrice> _itemSellPrices;
    [SerializeField] private List<BoxPrice> _boxPrices;


    public int BoxesInGroupAmount => 10;

    public float GetItemSellPrice(RarityType rarityType)
        => _itemSellPrices.Find(rarityPrice => rarityPrice.rarityType == rarityType).price;

    public float GetBoxPrice(BoxType boxType)
        => _boxPrices.Find(boxPrice => boxPrice.boxType == boxType).price;

}