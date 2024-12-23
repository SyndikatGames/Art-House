using PrizeClaw;
using R3;
using System.Collections.Generic;
using VG2;

public struct PrizeClawState
{
    public Dictionary<PrizeType, int> SpawnedPrizes;
    public ReactiveDictionary<PrizeType, int> Rewards;
    public ReactiveProperty<int> MovesLeft;

}
