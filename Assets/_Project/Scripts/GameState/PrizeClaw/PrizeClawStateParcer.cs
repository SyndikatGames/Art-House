using System.Collections.Generic;
using R3;
using VG2;


namespace PrizeClaw
{
    public class PrizeClawStateParcer : IStateParcer
    {
        private const string gameStartedKey = "pc_gameStarted";
        private const string spawnedPrizesKey = "pc_spawnedPrizes";
        private const string rewardsKey = "pc_rewards";
        private const string movesLeftKey = "pc_movesLeft";


        public void AddDataString(Dictionary<string, string> data)
        {
            data.Add(gameStartedKey, GameState.prizeClaw.gameStarted.ToString());
            data.Add(movesLeftKey, GameState.prizeClaw.movesLeft.ToString());

            int index = 0;
            string spawnedPrizesData = string.Empty;
            foreach (var item in GameState.prizeClaw.spawnedPrizes)
            {
                spawnedPrizesData += $"{(int)item.Key}_{item.Value}";
                if (index != GameState.prizeClaw.spawnedPrizes.Count - 1) spawnedPrizesData += ',';
                index++;
            }
            data.Add(spawnedPrizesKey, spawnedPrizesData);

            index = 0;
            string rewardsData = string.Empty;
            foreach (var item in GameState.prizeClaw.rewards)
            {
                rewardsData += $"{(int)item.Key}_{item.Value}";
                if (index != GameState.prizeClaw.rewards.Count - 1) rewardsData += ',';
                index++;
            }
            data.Add(rewardsKey, rewardsData);



        }

        public void Parse(Dictionary<string, string> data)
        {
            GameState.prizeClaw = new PrizeClawState();

            GameState.prizeClaw.gameStarted = data.ContainsKey(gameStartedKey) ? 
                bool.Parse(data[gameStartedKey]) : false;

            GameState.prizeClaw.movesLeft = new ReactiveProperty<int>(data.ContainsKey(movesLeftKey) ? 
                int.Parse(data[movesLeftKey]) : ConfigHub.PrizeClaw.MovesAmount);


            GameState.prizeClaw.spawnedPrizes = new Dictionary<PrizeType, int>();
            if (data.ContainsKey(spawnedPrizesKey) && data[spawnedPrizesKey] != string.Empty)
            {
                var splitData = data[spawnedPrizesKey].Split(',');
                foreach (var pairData in splitData)
                {
                    var splitPairData = pairData.Split('_');
                    GameState.prizeClaw.spawnedPrizes.Add
                        ((PrizeType)int.Parse(splitPairData[0]), int.Parse(splitPairData[1]));
                }
            }


            GameState.prizeClaw.rewards = new ReactiveDictionary<PrizeType, int>();
            if (data.ContainsKey(rewardsKey) && data[rewardsKey] != string.Empty)
            {
                var splitData = data[rewardsKey].Split(',');
                foreach (var pairData in splitData)
                {
                    var splitPairData = pairData.Split('_');
                    GameState.prizeClaw.rewards.Add
                        ((PrizeType)int.Parse(splitPairData[0]), int.Parse(splitPairData[1]));
                }
            }



        }
    }
}


