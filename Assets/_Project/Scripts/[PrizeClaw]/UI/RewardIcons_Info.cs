using UnityEngine;
using VG;

namespace PrizeClaw
{
    public class RewardIcons_Info : Info
    {

        protected override void Subscribe()
        {
            GameState.onChanged += UpdateValue;
        }

        protected override void Unsubscribe()
        {
            GameState.onChanged -= UpdateValue;
        }

        protected override void UpdateValue()
        {
            var prizesConfig = Configs.Prizes;
            var rewards = GameState.Current.Rewards;

            foreach (Transform child in transform)
                Destroy(child.gameObject);


            foreach (var reward in rewards)
            {
                Instantiate(prizesConfig.GetRewardIconPrefab(reward.Key), transform)
                    .SetAmount(reward.Value);
            }
        }



    }
}


