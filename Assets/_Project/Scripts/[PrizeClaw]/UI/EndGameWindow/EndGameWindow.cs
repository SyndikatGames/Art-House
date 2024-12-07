using UnityEngine;

namespace PrizeClaw
{
    public class EndGameWindow : MonoBehaviour
    {
        [SerializeField] private Transform _rewardGrid;


        private void OnEnable() => UpdateValues();


        private void UpdateValues()
        {
            var rewards = GameState.Current.Rewards;
            var prizesConfig = Configs.Prizes;

            foreach (var reward in rewards)
            {
                Instantiate(prizesConfig.GetRewardIconPrefab(reward.Key), _rewardGrid)
                    .SetAmount(reward.Value);
            }

        }


    }

}

