using VG;


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
                return $"{Localization.GetString("box_accumulation")}: " +
                    $"{value} {Localization.GetString("per_hour")}";

            case BonusType.IncreaseSellPricePercentage:
                return $"{Localization.GetString("sell_price")}: +{value}%";

            case BonusType.HourGemIncome:
                return $"{Localization.GetString("soft_money_income")}: " +
                    $"{value} {Localization.GetString("per_hour")}";

            case BonusType.IncreaseOfflineHours:
                return $"{Localization.GetString("max_offline_time")}: " +
                    $"{value} {Localization.GetString("hours")}";

            case BonusType.IncreaseProbabilityRareBox:
                return $"{Localization.GetString("rare_box_probability")}: {value}%";

            case BonusType.IncreaseProbabilityEpicBox:
                return $"{Localization.GetString("epic_box_probability")}: {value}%";

            case BonusType.IncreaseProbabilityFantasticBox:
                return $"{Localization.GetString("fantastic_box_probability")}: {value}%";
        }

        throw new System.Exception($"No description for {bonusType}");

    }
}
