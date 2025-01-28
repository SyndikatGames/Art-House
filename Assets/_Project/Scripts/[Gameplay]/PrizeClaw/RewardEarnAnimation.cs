using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using VG2;

namespace PrizeClaw
{
    public class RewardEarnAnimation : MonoBehaviour
    {
        public static bool Available { get; set; } = false;


        private async void Start()
        {
            if (!Available) return;

            Vector2 from = ScreenCalculator.GetScreenCenter();

            await Task.Delay(100);

            foreach (var rewards in GameState.prizeClaw.rewards)
            {
                print(rewards.Key);
                switch (rewards.Key)
                {
                    case PrizeType.Money:
                        new EarnAnimation(from, UI.MoneyValue, GameState.money.PreviousValue, 
                            GameState.money.Value, EarnAnimationType.Money);
                        break;

                    case PrizeType.CommonBox: case PrizeType.RareBox: case PrizeType.EpicBox:
                        const int MAX_IMAGES = 3;

                        var imageAmount = Mathf.Min(MAX_IMAGES, rewards.Value);
                        var sprite = ConfigHub.PrizeClaw.GetPrizeSprite(rewards.Key);
                        new EarnAnimation(from, UI.Shop, sprite, imageAmount);
                        break;
                }


            }

            Available = false;
        }


    }
}


