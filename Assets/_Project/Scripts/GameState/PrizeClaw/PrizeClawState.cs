using System.Collections.Generic;
using R3;
using VG2;

namespace PrizeClaw
{
    public class PrizeClawState
    {
        public bool gameStarted;
        public Dictionary<PrizeType, int> spawnedPrizes;
        public ReactiveDictionary<PrizeType, int> rewards;
        public ReactiveProperty<int> movesLeft;

    }
}


