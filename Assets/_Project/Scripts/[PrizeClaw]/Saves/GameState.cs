using System;
using System.Collections.Generic;
using VG;


namespace PrizeClaw
{
    public class GameState
    {
        public static event Action onChanged;


        private static GameState _currentState = null;
        public static GameState Current
        {
            get
            {
                if (_currentState == null)
                {
                    if (Saves.String[Key_Save.prize_claw_data].Value == string.Empty)
                        _currentState = CreateNewGameState();

                    else _currentState = ParseCurrentGameState();

                    _currentState.Save();
                }
                return _currentState;
            }
        }

        public Dictionary<PrizeType, int> SpawnedPrizes { get; private set; }
        public Dictionary<PrizeType, int> Rewards { get; private set; }
        public int Moves { get; private set; }

        public void Clear()
        {
            _currentState = null;
            Saves.String[Key_Save.prize_claw_data].Value = string.Empty;
        }

        public void SpendMove()
        {
            Moves--;
            Save();
        }

        public void AddMoves(int moves)
        {
            Moves += moves;
            Save();
        }

        public void AddReward(PrizeType prizeType, int amount)
        {
            if (Rewards.ContainsKey(prizeType))
                Rewards[prizeType] += amount;

            else Rewards.Add(prizeType, amount);

            Save();
        }

        public void RemovePrize(PrizeType prizeType)
        {
            SpawnedPrizes[prizeType]--;
            Save();
        }



        private static GameState CreateNewGameState()
        {
            var gameState = new GameState();
            var gameConfig = Configs.Game;
            gameState.SpawnedPrizes = gameConfig.Generate();
            gameState.Rewards = new Dictionary<PrizeType, int>();
            gameState.Moves = gameConfig.MovesAmount;

            return gameState;
        }

        private static GameState ParseCurrentGameState()
        {
            var gameState = new GameState();
            string[] splitData = Saves.String[Key_Save.prize_claw_data].Value.Split(';');

            gameState.Moves = int.Parse(splitData[0]);

            string[] splitPrizeData = splitData[1].Split(',');
            gameState.SpawnedPrizes = new Dictionary<PrizeType, int>();
            foreach (var prizeData in splitPrizeData)
            {
                var prizeType = (PrizeType)int.Parse(prizeData.Split('_')[0]);
                var amount = int.Parse(prizeData.Split('_')[1]);

                gameState.SpawnedPrizes.Add(prizeType, amount);
            }

            gameState.Rewards = new Dictionary<PrizeType, int>();

            if (splitData[2] != string.Empty)
            {
                string[] splitRewardData = splitData[2].Split(',');

                foreach (var rewardData in splitRewardData)
                {
                    var prizeType = (PrizeType)int.Parse(rewardData.Split('_')[0]);
                    var amount = int.Parse(rewardData.Split('_')[1]);

                    gameState.Rewards.Add(prizeType, amount);
                }
            }

            return gameState;
        }



        private void Save()
        {
            string data = $"{Moves};";

            int i = 0;
            foreach (var prize in SpawnedPrizes)
            {
                data += $"{(int)prize.Key}_{prize.Value}";
                if (i != SpawnedPrizes.Count - 1) data += ",";
                i++;
            }
            data += ";";

            i = 0;
            foreach (var reward in Rewards)
            {
                data += $"{(int)reward.Key}_{reward.Value}";
                if (i != Rewards.Count - 1) data += ",";
                i++;
            }

            Saves.String[Key_Save.prize_claw_data].Value = data;
            onChanged?.Invoke();
        }

        

    }
}


