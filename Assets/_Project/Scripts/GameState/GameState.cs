using System;
using System.Collections.Generic;
using PrizeClaw;
using R3;

namespace VG2
{
    public static class GameState
    {
        public const int roomsAmount = 1;

        public static ReactiveProperty<bool> adsEnabled;
        public static DateTime lastOnlineTime;
        public static DateTime lastIncomeTime;

        public static ReactiveProperty<int> tutorialStepIndex;

        public static MemoryReactiveProperty<float> money;
        public static ReactiveProperty<int> currentRoomIndex;

        public static List<RoomState> roomStates;

        public static ReactiveProperty<float> tickets;
        public static PrizeClawState prizeClaw;




        public static RoomState CurrentRoom => roomStates[currentRoomIndex.Value];



    }
}

