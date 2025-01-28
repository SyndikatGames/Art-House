using System.Collections.Generic;
using UnityEngine;


namespace PrizeClaw
{

    [CreateAssetMenu(menuName = "Project/PrizeClaw", fileName = "PrizeClaw")]
    public class PrizeClawConfig : ScriptableObject
    {

        [System.Serializable]
        private struct PrizeData
        {
            public PrizeType prizeType;
            public float probabilityPercentage;
            public Sprite sprite;
        }


        [field: SerializeField] public LaunchGameWindowView LaunchGameWindow { get; private set; }

        [field: SerializeField] public float MoneyInsideReward { get; private set; }
        [field: SerializeField] public float TicketsPerHour { get; private set; }
        [field: SerializeField] public int MovesAmount { get; private set; }
        [field: SerializeField] public int PrizesAmount { get; private set; }

        [SerializeField] private List<PrizeData> _prizeProbabilities; 

        [SerializeField] private List<Prize> _prizePrefabs;
        [SerializeField] private List<RewardIcon> _rewardIcons;

        public Prize GetPrizePrefab(PrizeType prizeType)
            => _prizePrefabs.Find((prizePrefab) => prizePrefab.PrizeType == prizeType);

        public RewardIcon GetRewardIconPrefab(PrizeType prizeType)
            => _rewardIcons.Find((prizePrefab) => prizePrefab.PrizeType == prizeType);


        public Dictionary<PrizeType, float> GetPrizeProbabilitiesPercentages()
        {
            var result = new Dictionary<PrizeType, float>();
            foreach (var item in _prizeProbabilities)
                result.Add(item.prizeType, item.probabilityPercentage);

            return result;
        }

        public Sprite GetPrizeSprite(PrizeType prizeType)
            => _prizeProbabilities.Find(item =>  item.prizeType == prizeType).sprite;


    }

}






