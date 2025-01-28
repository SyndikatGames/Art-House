using VG2;


namespace PrizeClaw
{
    public static class RewardCalculator
    {

        public static void AcceptPrize(PrizeType prizeType)
        {
            if (GameState.prizeClaw.rewards.ContainsKey(prizeType))
                GameState.prizeClaw.rewards.Set(prizeType, GameState.prizeClaw.rewards.Get(prizeType) + 1);

            else GameState.prizeClaw.rewards.Add(prizeType, 1);

            GameState.prizeClaw.spawnedPrizes[prizeType]--;
            if (GameState.prizeClaw.spawnedPrizes[prizeType] == 0)
                GameState.prizeClaw.spawnedPrizes.Remove(prizeType);
        }


    }
}



