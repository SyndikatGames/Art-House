using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Project/Basic Rules", fileName = "BasicRules")]
public class BasicRulesConfig : ScriptableObject
{
    [System.Serializable]
    public struct RarityPrice
    {
        public RarityType rarityType;
        public int price;
    }

    [System.Serializable]
    public struct RarityPrestige
    {
        public RarityType rarityType;
        public int prestige;
    }

    [System.Serializable]
    public struct BoxProbability
    {
        public RarityType rarityType;
        public float probabilityPercentage;
    }


    [field: SerializeField] public float OfflineHoursLimit { get; private set; }
    [field: SerializeField] public float BoxesPerHour { get; private set; }
    [field: SerializeField] public List<RarityPrice> SellPrices { get; private set; }
    [field: SerializeField] public List<BoxProbability> BoxProbabilities { get; private set; }
    [field: SerializeField] public List<RarityPrestige> RarityPrestiges { get; private set; }

}

public static partial class Configs
{
    private static BasicRulesConfig _basicRules;
    public static BasicRulesConfig BasicRules
    {
        get
        {
            if (_basicRules == null)
                _basicRules = Resources.Load<BasicRulesConfig>($"BasicRules");

            return _basicRules;
        }
    }
        
}