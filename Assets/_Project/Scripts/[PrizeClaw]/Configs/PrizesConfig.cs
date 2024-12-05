using System.Collections.Generic;
using UnityEngine;


namespace PrizeClaw
{
    [CreateAssetMenu(menuName = "PrizeClaw/Prizes", fileName = "Prizes")]
    public class PrizesConfig : ScriptableObject
    {
        [SerializeField] private List<Prize> _prizePrefabs;
        [SerializeField] private List<RewardIcon> _rewardIcons;

        public Prize GetPrizePrefab(PrizeType prizeType)
            => _prizePrefabs.Find((prizePrefab) => prizePrefab.PrizeType == prizeType);

        public RewardIcon GetRewardIconPrefab(PrizeType prizeType)
            => _rewardIcons.Find((prizePrefab) => prizePrefab.PrizeType == prizeType);

    }

    public static partial class Configs
    {
        public static PrizesConfig Prizes =>
            Resources.Load<PrizesConfig>("PrizeClaw/Prizes");
    }
}



