
public enum BonusType
{
    IncreaseBoxesPerHour = 1,
    IncreaseSellPricePercentage,
    HourGemIncome,
    IncreaseOfflineHours,
    IncreasePrestigePercentage,

    IncreaseProbabilityRareBox = 10,
    IncreaseProbabilityEpicBox,
    IncreaseProbabilityFantasticBox,
}


public static class BonusDescription
{
    public static string Get(BonusType bonusType, float value)
    {
        switch (bonusType)
        {
            case BonusType.IncreaseBoxesPerHour:
                return $"{BonusType.IncreaseBoxesPerHour}: +{value}";

            case BonusType.IncreaseSellPricePercentage:
                return $"{BonusType.IncreaseSellPricePercentage}: {value}";

            case BonusType.HourGemIncome:
                return $"{BonusType.HourGemIncome}: {value}";

            case BonusType.IncreaseOfflineHours:
                return $"{BonusType.IncreaseOfflineHours}: {value}";

            case BonusType.IncreasePrestigePercentage:
                return $"{BonusType.IncreasePrestigePercentage}: {value}";

            case BonusType.IncreaseProbabilityRareBox:
                return $"{BonusType.IncreaseProbabilityRareBox}: {value}";

            case BonusType.IncreaseProbabilityEpicBox:
                return $"{BonusType.IncreaseProbabilityEpicBox}: {value}";

            case BonusType.IncreaseProbabilityFantasticBox:
                return $"{BonusType.IncreaseProbabilityFantasticBox}: {value}";
        }

        throw new System.Exception($"No description for {bonusType}");

    }
}
