using System.Collections.Generic;
using UnityEngine;

public enum BonusType
{
    IncreaseBoxLimit = 0,
    DecreaseBoxEveryMinutes,
    IncreaseSellPricePercentage,
    IncreaseHourIncome,
    IncreaseIncomeHourLimit,

    IncreaseProbabilityRareBox = 10,
    IncreaseProbabilityEpicBox,
    IncreaseProbabilityFantasticBox,
}

[System.Serializable]
public struct BonusValue
{
    public BonusType type;
    public float value;
}


[CreateAssetMenu(menuName = "Project/Room Bonuses", fileName = "RoomBonuses")]
public class RoomBonusesConfig : ScriptableObject
{
    [System.Serializable]
    private struct LevelRoomBonuses
    {
        public List<BonusValue> bonusList;
    }

    [SerializeField] private List<LevelRoomBonuses> _levelBonuses;


    public List<BonusValue> GetBonuses(int level)
        => _levelBonuses[level].bonusList;

}

public static partial class Configs
{
    public static RoomBonusesConfig GetRoomBonuses(int roomNumber) =>
        Resources.Load<RoomBonusesConfig>($"RoomBonuses/{roomNumber}");
}



