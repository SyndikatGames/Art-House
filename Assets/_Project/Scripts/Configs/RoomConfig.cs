using System.Collections.Generic;
using UnityEngine;
using VG;






[CreateAssetMenu(menuName = "Project/Room", fileName = "Room")]
public class RoomConfig : ScriptableObject
{
    [System.Serializable]
    private struct BonusValue
    {
        public BonusType type;
        public float value;
    }

    [System.Serializable]
    private struct LevelRoomBonuses
    {
        public List<BonusValue> bonusList;
    }


    [SerializeField] private List<int> _prestigeRequires;
    [SerializeField] private List<LevelRoomBonuses> _levelBonuses;

    private int RoomIndex => int.Parse(name);

    public int GetCurrentPrestigeRequire() => _prestigeRequires[CurrentLevel];

    public Dictionary<BonusType, float> GetNewLevelBonuses(int level)
    {
        var result = new Dictionary<BonusType, float>();
        for (int i = 0; i < _levelBonuses[level].bonusList.Count; i++)
            result.Add(_levelBonuses[level].bonusList[i].type, 
                _levelBonuses[level].bonusList[i].value);

        return result;
    }

    public Dictionary<BonusType, float> CurrentBonuses
    {
        get
        {
            var result = new Dictionary<BonusType, float>();
            int roomLevel = CurrentLevel;

            for (int i = 0; i <= roomLevel; i++)
            {
                foreach (var bonus in _levelBonuses[i].bonusList)
                {
                    if (result.ContainsKey(bonus.type))
                        result[bonus.type] += bonus.value;

                    else result.Add(bonus.type, bonus.value);
                }
            }

            return result;
        }
    }


    public int CurrentLevel
    {
        get
        {
            
            int prestige = Saves.GetPrestige(RoomIndex);
            Debug.Log(prestige);

            for (int level = 0; level < _prestigeRequires.Count; level++)
            {
                if (prestige < _prestigeRequires[level])
                    return level;

                prestige -= _prestigeRequires[level];
            }
            return _prestigeRequires.Count;
        }
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



