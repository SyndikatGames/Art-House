using UnityEngine;
using VG2;

namespace PrizeClaw
{
    public class EndGameWindowView : MonoBehaviour
    {
        [SerializeField] private Transform _rewardGrid;


        private void OnEnable() => UpdateValues();


        private void UpdateValues()
        {
            var rewards = GameState.prizeClaw.rewards;

            foreach (var reward in rewards)
            {
                Instantiate(ConfigHub.PrizeClaw.GetRewardIconPrefab(reward.Key), _rewardGrid)
                    .SetAmount(reward.Value);
            }

        }


    }

}

