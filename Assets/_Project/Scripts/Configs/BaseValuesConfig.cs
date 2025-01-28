using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Project/Base Values", fileName = "BaseValues")]
public class BaseValuesConfig : ScriptableObject
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

    [System.Serializable]
    private struct RarityPrestigePoints
    {
        public RarityType rarityType;
        public float prestigePoints;
    }

    [field: SerializeField] public float MaxOfflineHours { get; private set; }
    [SerializeField] private List<float> _roomLevelPrestigePointsRequires;
    [SerializeField] private List<float> _roomExpansionPrices;
    [SerializeField] private List<RarityPrestigePoints> _rarityPrestigePoints;
    [SerializeField] private List<RarityPrice> _itemSellPrices;
    [SerializeField] private List<BoxPrice> _boxPrices;


    public float GetRoomExpansionPrice(int expansionLevel) 
        => _roomExpansionPrices[expansionLevel - 1];


    public float GetRoomLevelPrestigePointsRequire(int roomLevel)
    {
        int levelIndex = roomLevel - 1;
        if (levelIndex < _roomLevelPrestigePointsRequires.Count)
            return _roomLevelPrestigePointsRequires[levelIndex];

        int lastIndex = _roomLevelPrestigePointsRequires.Count - 1;
        return _roomLevelPrestigePointsRequires[lastIndex];
    }


    public float GetItemPrestigePoints(RarityType rarityType) 
        => _rarityPrestigePoints.Find(rarityPoints => rarityPoints.rarityType == rarityType).prestigePoints;

    public float GetItemSellPrice(RarityType rarityType)
        => _itemSellPrices.Find(rarityPrice => rarityPrice.rarityType == rarityType).price;

    public float GetBoxPrice(BoxType boxType)
        => _boxPrices.Find(boxPrice => boxPrice.boxType == boxType).price;



}
