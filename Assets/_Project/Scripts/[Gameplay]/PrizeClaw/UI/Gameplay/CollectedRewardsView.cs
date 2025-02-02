using UnityEngine;
using VG2;
using R3;

namespace PrizeClaw
{
    public class CollectedRewardsView : ReactiveView
    {

        protected override void Subscribe()
        {
            disposables.Add(GameState.prizeClaw.rewards.OnChanged.Subscribe(_ => Display()));
        }

        protected override void Display()
        {
            var rewards = GameState.prizeClaw.rewards;

            foreach (Transform child in transform)
                Destroy(child.gameObject);

            foreach (var reward in rewards)
            {
                Instantiate(ConfigHub.PrizeClaw.GetRewardIconPrefab(reward.Key), transform)
                    .SetAmount((int)reward.Value);
            }
        }



    }
}


