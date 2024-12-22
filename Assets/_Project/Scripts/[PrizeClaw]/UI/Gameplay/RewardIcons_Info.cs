using UnityEngine;
using VG;

namespace PrizeClaw
{
    public class RewardIcons_Info : ReactiveView
    {

        protected override void Subscribe()
        {
            GameState.onChanged += Display;
        }

        protected override void Dispose()
        {
            GameState.onChanged -= Display;
        }

        protected override void Display()
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


