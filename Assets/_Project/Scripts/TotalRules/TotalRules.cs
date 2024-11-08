using System.Collections.Generic;

public static class TotalRules
{
    public static int BoxLimit { get; private set; }
    public static float BoxEveryMinutes { get; private set; }
    public static Dictionary<RarityType, float> BoxProbabilities { get; private set; }

    private static Dictionary<RarityType, int> _sellPrices;

    public static int GetSellPrice(RarityType rarityType) => _sellPrices[rarityType];


    public static void Update()
    {
        var basicRules = Configs.BasicRules;

        BoxLimit = basicRules.BoxLimit;
        BoxEveryMinutes = basicRules.BoxEveryMinutes;

        BoxProbabilities = new Dictionary<RarityType, float>();
        foreach (var boxProbability in basicRules.BoxProbabilities)
            BoxProbabilities.Add(boxProbability.rarityType, boxProbability.probabilityPercentage / 100f);

        _sellPrices = new Dictionary<RarityType, int>();
        foreach (var sellPrice in basicRules.SellPrices)
            _sellPrices.Add(sellPrice.rarityType, sellPrice.price);
    }


}
