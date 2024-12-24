using System;
using System.Collections.Generic;
using R3;
using VG2;

namespace VG2
{
    public static class GameStateParcer
    {
        private static string NewStyleIndicesKey(int roomIndex) => $"newStyleIndices{roomIndex}";
        private static string CurrentStyleIndexKey(int roomIndex) => $"currentStyleIndex{roomIndex}";
        private static string BoxesAmountKey(int roomIndex) => $"boxesAmount{roomIndex}";
        private static string AccumulatedMoneyKey(int roomIndex) => $"accumulatedMoney{roomIndex}";

        private const string currentRoomIndexKey = "currentRoomIndex";
        private const string styleTutorialCompletedKey = "styleTutorialCompleted";
        private const string tutorialStepKey = "tutorialStep";
        private const string tutorialCompletedKey = "tutorialCompleted";
        private const string lastOnlineTimeKey = "lastOnlineTime";
        private const string adsEnabledKey = "adsEnabled";
        private const string moneyKey = "money";


        public static void Parse(StartValuesConfig startValuesConfig, Dictionary<string, string> data)
        {
            GameState.lastOnlineTime = data.ContainsKey(lastOnlineTimeKey) ?
                DateTime.Parse(data[lastOnlineTimeKey]) : DateTime.Now;

            GameState.adsEnabled = new ReactiveProperty<bool>
                (data.ContainsKey(adsEnabledKey) ? bool.Parse(data[adsEnabledKey]) : true);

            GameState.money = new ReactiveProperty<float>
                (data.ContainsKey(moneyKey) ? float.Parse(data[moneyKey]) : 0);

            GameState.tutorialCompleted = data.ContainsKey(tutorialCompletedKey) ?
                bool.Parse(data[tutorialCompletedKey]) : false;

            GameState.styleTutorialCompleted = data.ContainsKey(styleTutorialCompletedKey) ?
                bool.Parse(data[styleTutorialCompletedKey]) : false;

            GameState.tutorialStep = new ReactiveProperty<int> 
                (data.ContainsKey(tutorialStepKey) ? int.Parse(data[tutorialStepKey]) : 0);

            GameState.currentRoomIndex = new ReactiveProperty<int>
                (data.ContainsKey(currentRoomIndexKey) ? int.Parse(data[currentRoomIndexKey]) : 0);

            // Room states
            for (int roomIndex = 0; roomIndex < GameState.roomsAmount; roomIndex++)
            {
                var roomState = new RoomState();

                roomState.currentStyleIndex = new ReactiveProperty<int>
                    (data.ContainsKey(CurrentStyleIndexKey(roomIndex)) ?
                    int.Parse(data[CurrentStyleIndexKey(roomIndex)]) : 0);

                roomState.newStyleIndices = new ReactiveList<int>();
                for (int styleIndex = 0; styleIndex < GameState.stylesAmount; styleIndex++)
                    if (data.ContainsKey(NewStyleIndicesKey(roomIndex)))
                    {
                        var splitData = data[NewStyleIndicesKey(roomIndex)].Split('_');
                        roomState.newStyleIndices.Add(int.Parse(splitData[0]));
                    }

                roomState.boxesAmount = new ReactiveDictionary<BoxType, int>();
                if (data.ContainsKey(BoxesAmountKey(roomIndex)))
                {
                    var splitData = data[BoxesAmountKey(roomIndex)].Split('_');
                    foreach (var pairData in splitData)
                    {
                        var splitPairData = pairData.Split(',');
                        roomState.boxesAmount.Add
                            ((BoxType)int.Parse(splitPairData[0]), int.Parse(splitPairData[1]));
                    }

                }

                roomState.accumulatedMoney = new ReactiveProperty<float>
                    (data.ContainsKey(AccumulatedMoneyKey(roomIndex)) ?
                    float.Parse(data[AccumulatedMoneyKey(roomIndex)]) : 0f);



            }
        }


        public static Dictionary<string, string> ToDataString()
        {
            var data = new Dictionary<string, string>();

            data.Add(lastOnlineTimeKey, GameState.lastOnlineTime.ToString());
            data.Add(adsEnabledKey, GameState.adsEnabled.ToString());
            data.Add(moneyKey, GameState.money.ToString());
            data.Add(tutorialCompletedKey, GameState.tutorialCompleted.ToString());
            data.Add(styleTutorialCompletedKey, GameState.styleTutorialCompleted.ToString());
            data.Add(tutorialStepKey, GameState.tutorialStep.ToString());
            data.Add(currentRoomIndexKey, GameState.currentRoomIndex.ToString());

            // Room states
            for (int roomIndex = 0; roomIndex < GameState.roomsAmount; roomIndex++)
            {
                var roomState = GameState.roomStates[roomIndex];

                data.Add(CurrentStyleIndexKey(roomIndex), roomState.currentStyleIndex.ToString());

                var styleData = string.Empty;
                for (int i = 0; i < roomState.newStyleIndices.Count; i++)
                {
                    styleData += roomState.newStyleIndices.Get(i).ToString();
                    if (i != roomState.newStyleIndices.Count - 1) styleData += '_';
                }
                data.Add(NewStyleIndicesKey(roomIndex), styleData);

                int index = 0;
                var boxData = string.Empty;
                foreach (var boxAmount in roomState.boxesAmount.ToArray())
                {
                    boxData += $"{(int)boxAmount.Key},{boxAmount.Value}";
                    if (index != roomState.boxesAmount.Count - 1) boxData += "_";
                    index++;
                }

                data.Add(AccumulatedMoneyKey(roomIndex), roomState.accumulatedMoney.ToString());

            }




            return data;
        }


    }
}


