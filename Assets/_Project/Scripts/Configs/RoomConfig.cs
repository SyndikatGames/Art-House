using System.Collections.Generic;
using UnityEngine;
using VG;

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


[CreateAssetMenu(menuName = "Project/Room", fileName = "Room")]
public class RoomConfig : ScriptableObject
{
    [System.Serializable]
    private struct LevelRoomBonuses
    {
        public List<BonusValue> bonusList;
    }

    [SerializeField] private List<int> _prestigeRequires;
    [SerializeField] private List<LevelRoomBonuses> _levelBonuses;

    private int RoomIndex => int.Parse(name);

    public int GetCurrentPrestigeRequire()
        => _prestigeRequires[GetCurrentLevel() - 1];

    public List<BonusValue> GetBonuses(int level)
        => _levelBonuses[level].bonusList;

    public int GetCurrentLevel()
    {
        int prestige = Saves.GetPrestige(RoomIndex);

        for (int levelIndex = 0; levelIndex < _prestigeRequires.Count; levelIndex++)
        {
            if (prestige < _prestigeRequires[levelIndex]) 
                return levelIndex + 1;

            prestige -= _prestigeRequires[levelIndex];
        }
        return _prestigeRequires.Count;
    }

    public int GetCurrentPrestige()
    {
        int prestige = Saves.GetPrestige(RoomIndex);

        for (int levelIndex = 0; levelIndex < _prestigeRequires.Count; levelIndex++)
        {
            if (prestige < _prestigeRequires[levelIndex])
                return prestige;

            prestige -= _prestigeRequires[levelIndex];
        }
        return _prestigeRequires[_prestigeRequires.Count - 1];
    }

}

public static partial class Configs
{
    public static RoomConfig GetRoom(int index) =>
        Resources.Load<RoomConfig>($"Rooms/{index}");
}



