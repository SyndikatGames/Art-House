
public enum BonusType
{
    IncreaseBoxesPerHour = 1,
    IncreaseSellPricePercentage,
    HourGemIncome,
    IncreaseOfflineHours,

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
                return $"Накопление коробок: {value} в час";

            case BonusType.IncreaseSellPricePercentage:
                return $"Стоимость продажи: +{value}%";

            case BonusType.HourGemIncome:
                return $"Алмазный доход: {value} в час";

            case BonusType.IncreaseOfflineHours:
                return $"Макс. время отсутствия: {value} часов";

            case BonusType.IncreaseProbabilityRareBox:
                return $"Шанс редкой коробки: {value}%";

            case BonusType.IncreaseProbabilityEpicBox:
                return $"Шанс эпической коробки: {value}%";

            case BonusType.IncreaseProbabilityFantasticBox:
                return $"Шанс фантастической коробки: {value}%";
        }

        throw new System.Exception($"No description for {bonusType}");

    }
}
