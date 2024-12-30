using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Project/Unboxing", fileName = "Unboxing")]
public class UnboxingConfig : ScriptableObject
{
    [System.Serializable]
    private struct RarityProbability
    {
        public RarityType rarityType;
        public float value;
    }


    [System.Serializable]
    private struct BoxInfo
    { 
        public BoxType boxType;
        public Sprite boxSprite;
        public Sprite openedBoxSprite;
        public Color textColor;
        public List<RarityProbability> probabilities;
    }

    [field: SerializeField] public UnboxingView UnboxingWindowPrefab { get; private set; }

    [SerializeField] private List<BoxInfo> _boxInfoList;


    public Color GetBoxTextColor(BoxType boxType) 
        => _boxInfoList.Find(item => item.boxType == boxType).textColor;

    public Sprite GetBoxSprite(BoxType boxType) 
        => _boxInfoList.Find(item => item.boxType == boxType).boxSprite;

    public Sprite GetOpenedBoxSprite(BoxType boxType)
        => _boxInfoList.Find(item => item.boxType == boxType).openedBoxSprite;

    public Dictionary<RarityType, float> GetRarityProbabilites(BoxType boxType)
    {
        float checkSum = 0f;

        var result = new Dictionary<RarityType, float>();
        var boxProbabilities = _boxInfoList.Find(item => item.boxType == boxType);

        foreach (var probability in boxProbabilities.probabilities)
        {
            result.Add(probability.rarityType, probability.value);
            checkSum += probability.value;
        }

        if (checkSum < 99.9f || 100.1f < checkSum)
            throw new System.Exception
                ($"[{nameof(UnboxingConfig)}]: Sum of probabilities is not equal 100%");


        return result;
    }
    


}
