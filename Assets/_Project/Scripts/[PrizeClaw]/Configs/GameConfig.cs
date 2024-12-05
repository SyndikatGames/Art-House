using System.Collections.Generic;
using UnityEngine;


namespace PrizeClaw
{

    [CreateAssetMenu(menuName = "PrizeClaw/Game", fileName = "Game")]
    public class GameConfig : ScriptableObject
    {
        [System.Serializable]
        private struct PrizeData
        {
            public PrizeType prizeType;
            public float probabilityPercentage;
        }

        [field: SerializeField] public int MovesAmount { get; private set; } 
        [SerializeField] private int _prizesAmount;
        [SerializeField] private List<PrizeData> _prizeProbabilities;


        public Dictionary<PrizeType, int> Generate()
        {
            var dictionary = new Dictionary<PrizeType, int>();

            float totalPercentage = 0;
            foreach (var prizeProbability in _prizeProbabilities)
                totalPercentage += prizeProbability.probabilityPercentage;


            for (int i = 0; i < _prizesAmount; i++)
            {
                float from = 0f;
                float random = Random.Range(0f, totalPercentage);

                foreach (var prizeProbability in _prizeProbabilities)
                {
                    if (from <= random && random <= from + prizeProbability.probabilityPercentage)
                    {
                        if (dictionary.ContainsKey(prizeProbability.prizeType))
                            dictionary[prizeProbability.prizeType]++;

                        else dictionary.Add(prizeProbability.prizeType, 1);
                        break;
                    }

                    from += prizeProbability.probabilityPercentage;
                }
            }

            return dictionary;
        }


    }

    public static partial class Configs
    {
        public static GameConfig Game =>
            Resources.Load<GameConfig>("PrizeClaw/Game");
    }

}






