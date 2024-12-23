using System;
using System.Collections.Generic;
using R3;

namespace VG2
{
    public class GameState
    {
        public const int roomsAmount = 1;
        public const int stylesAmount = 10;



        public ReactiveProperty<bool> adsEnabled;
        public DateTime lastOnlineTime;

        public bool tutorialCompleted;
        public bool styleTutorialCompleted;
        public int tutorialStep;

        public ReactiveProperty<float> money;
        public ReactiveProperty<int> currentRoomIndex;

        public List<RoomState> roomStates;

        public ReactiveProperty<float> prizeClawTickets;
        public PrizeClawState prizeClawState;

    }
}

