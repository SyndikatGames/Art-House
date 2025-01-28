using System.Collections.Generic;
using UnityEngine;
using VG2;
using Zenject;


namespace PrizeClaw
{
    public class GameLogicController : IInitializable
    {

        public void Initialize()
        {
            if (GameState.prizeClaw.gameStarted == false)
            {
                GameState.prizeClaw.spawnedPrizes = GeneratePrizes();
                GameState.prizeClaw.movesLeft.Value = ConfigHub.PrizeClaw.MovesAmount;
                GameState.prizeClaw.rewards.Clear();
                GameState.prizeClaw.gameStarted = true;
            }
                
        }


        private Dictionary<PrizeType, int> GeneratePrizes()
        {
            var dictionary = new Dictionary<PrizeType, int>();

            float finishProbability = 0;
            foreach (var item in ConfigHub.PrizeClaw.GetPrizeProbabilitiesPercentages())
                finishProbability += item.Value;

            for (int i = 0; i < ConfigHub.PrizeClaw.PrizesAmount; i++)
            {
                float from = 0f;
                float random = Random.Range(0f, finishProbability);

                foreach (var prizeProbability in ConfigHub.PrizeClaw.GetPrizeProbabilitiesPercentages())
                {
                    float to = from + prizeProbability.Value;

                    if (from <= random && random <= to)
                    {
                        if (dictionary.ContainsKey(prizeProbability.Key))
                            dictionary[prizeProbability.Key]++;

                        else dictionary.Add(prizeProbability.Key, 1);
                        break;
                    }

                    from = to;
                }
            }

            return dictionary;
        }





        
    }
}




