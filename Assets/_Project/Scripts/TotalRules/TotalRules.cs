using System.Collections.Generic;
using UnityEngine;
using VG;

public static class TotalRules
{
    public static bool Initialized { get; private set; } = false;

    public static float HourGemIncome { get; private set; }

    private static Dictionary<RarityType, float> _boxesProbabilityPercentages;

    private static Dictionary<RarityType, float> _sellPrices;

    public static float GetItemSellPrice(RarityType rarityType) => _sellPrices[rarityType];

    public static int GetItemPrestige(RarityType rarityType) 
        => Configs.BasicRules.RarityPrestiges
        .Find((prestige) => prestige.rarityType == rarityType).prestige;


    public static float GetOfflineHoursLimit(int roomIndex)
    {
        float result = Configs.BasicRules.OfflineHoursLimit;
        var roomBonuses = Configs.GetRoom(roomIndex).CurrentBonuses;

        if (roomBonuses.ContainsKey(BonusType.IncreaseOfflineHours))
            result += roomBonuses[BonusType.IncreaseOfflineHours];

        return result;
    }

    public static float GetBoxesPerHour(int roomIndex)
    {
        float result = Configs.BasicRules.BoxesPerHour;
        var roomBonuses = Configs.GetRoom(roomIndex).CurrentBonuses;

        if (roomBonuses.ContainsKey(BonusType.IncreaseBoxesPerHour))
            result += roomBonuses[BonusType.IncreaseBoxesPerHour];

        return result;
    }


    public static RarityType GenerateTimeBoxRarity()
    {
        float value = Random.Range(0f, 100f);
        float from = 0f;

        foreach (var probability in _boxesProbabilityPercentages)
        {
            float to = from + probability.Value;

            if (from <= value && value <= to)
                return probability.Key;

            from = to;
        }

        return RarityType.Common;
    }


    public static void Update()
    {
        var basicRules = Configs.BasicRules;

        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;
        var roomBonuses = Configs.GetRoom(roomIndex).CurrentBonuses;

        HourGemIncome = 0f;
        if (roomBonuses.ContainsKey(BonusType.HourGemIncome))
            HourGemIncome = roomBonuses[BonusType.HourGemIncome];

        _sellPrices = new Dictionary<RarityType, float>();
        foreach (var basicSellPrice in basicRules.SellPrices)
        {
            float result = basicSellPrice.price;
            if (roomBonuses.ContainsKey(BonusType.IncreaseSellPricePercentage))
                result *= 1 + roomBonuses[BonusType.IncreaseSellPricePercentage] / 100f;

            _sellPrices.Add(basicSellPrice.rarityType, result);
        }

        _boxesProbabilityPercentages = new Dictionary<RarityType, float>();
        foreach (var boxProbability in basicRules.BoxProbabilities)
        {
            float result = boxProbability.probabilityPercentage;
            switch (boxProbability.rarityType)
            {
                case RarityType.Rare:
                    if (roomBonuses.ContainsKey(BonusType.IncreaseProbabilityRareBox))
                        result += roomBonuses[BonusType.IncreaseProbabilityRareBox];
                    break;

                case RarityType.Epic:
                    if (roomBonuses.ContainsKey(BonusType.IncreaseProbabilityEpicBox))
                        result += roomBonuses[BonusType.IncreaseProbabilityEpicBox];
                    break;

                case RarityType.Fantastic:
                    if (roomBonuses.ContainsKey(BonusType.IncreaseProbabilityFantasticBox))
                        result += roomBonuses[BonusType.IncreaseProbabilityFantasticBox];
                    break;
            }

            _boxesProbabilityPercentages.Add(boxProbability.rarityType, result);
        }

        Initialized = true;
    }


}
