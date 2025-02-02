using System;
using System.Collections.Generic;
using PrizeClaw;
using R3;

namespace VG2
{
    public static class GameStateParcer
    {
        private const string currentRoomIndexKey = "currentRoomIndex";
        private const string styleTutorialCompletedKey = "styleTutorialCompleted";
        private const string tutorialStepKey = "tutorialStep";
        private const string tutorialCompletedKey = "tutorialCompleted";
        private const string lastOnlineTimeKey = "lastOnlineTime";
        private const string lastIncomeTimeKey = "lastIncomeTime";
        private const string adsEnabledKey = "adsEnabled";
        private const string moneyKey = "money";
        private const string ticketsKey = "tickets";


        public static void Parse(StartValuesConfig startValuesConfig, Dictionary<string, string> data)
        {
            GameState.lastOnlineTime = data.ContainsKey(lastOnlineTimeKey) ?
                DateTime.Parse(data[lastOnlineTimeKey]) : DateTime.Now;

            GameState.adsEnabled = new ReactiveProperty<bool>
                (data.ContainsKey(adsEnabledKey) ? bool.Parse(data[adsEnabledKey]) : true);

            GameState.money = new MemoryReactiveProperty<float>
                (data.ContainsKey(moneyKey) ? float.Parse(data[moneyKey]) : 0);

            GameState.tutorialStepIndex = new ReactiveProperty<int> 
                (data.ContainsKey(tutorialStepKey) ? int.Parse(data[tutorialStepKey]) : 0);

            GameState.currentRoomIndex = new ReactiveProperty<int>
                (data.ContainsKey(currentRoomIndexKey) ? int.Parse(data[currentRoomIndexKey]) : 0);

            GameState.tickets = new ReactiveProperty<float>
                (data.ContainsKey(ticketsKey) ? float.Parse(data[ticketsKey]) : 3f);

            GameState.lastIncomeTime = data.ContainsKey(lastIncomeTimeKey) ?
                DateTime.Parse(data[lastIncomeTimeKey]) : DateTime.Now;

            new RoomStateParcer().Parse(data);
            new PrizeClawStateParcer().Parse(data);

        }


        public static Dictionary<string, string> ToDataString()
        {
            var data = new Dictionary<string, string>();

            data.Add(lastOnlineTimeKey, GameState.lastOnlineTime.ToString());
            data.Add(lastIncomeTimeKey, GameState.lastIncomeTime.ToString());

            data.Add(adsEnabledKey, GameState.adsEnabled.ToString());
            data.Add(moneyKey, GameState.money.ToString());
            data.Add(tutorialStepKey, GameState.tutorialStepIndex.ToString());
            data.Add(currentRoomIndexKey, GameState.currentRoomIndex.ToString());
            data.Add(ticketsKey, GameState.tickets.ToString());

            new RoomStateParcer().AddDataString(data);
            new PrizeClawStateParcer().AddDataString(data);



            return data;
        }


    }
}


