using System;
using System.Collections.Generic;
using R3;

namespace VG2
{
    public static class GameState
    {
        public const int roomsAmount = 1;



        public static ReactiveProperty<bool> adsEnabled;
        public static DateTime lastOnlineTime;

        public static bool tutorialCompleted;
        public static bool styleTutorialCompleted;
        public static ReactiveProperty<int> tutorialStep;

        public static ReactiveProperty<float> money;
        public static ReactiveProperty<int> currentRoomIndex;

        public static List<RoomState> roomStates;

        public static ReactiveProperty<float> prizeClawTickets;
        public static PrizeClawState prizeClaw;




        public static RoomState CurrentRoom => roomStates[currentRoomIndex.Value];



    }
}

