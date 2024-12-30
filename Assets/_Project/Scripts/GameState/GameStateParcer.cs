using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace VG2
{
    public static class GameStateParcer
    {
        private static string NewStyleIndicesKey(int roomIndex) => $"newStyleIndices{roomIndex}";
        private static string CurrentStyleIndexKey(int roomIndex) => $"currentStyleIndex{roomIndex}";
        private static string BoxesAmountKey(int roomIndex) => $"boxesAmount{roomIndex}";
        private static string AccumulatedMoneyKey(int roomIndex) => $"accumulatedMoney{roomIndex}";
        private static string ItemsHierarchyKey(int roomIndex) => $"itemsHierarchy{roomIndex}";
        private static string RoomSizeKey(int roomIndex) => $"roomSize{roomIndex}";
        private static string CardsKey(int roomIndex) => $"cards{roomIndex}";
        private static string MaxReachedLevel(int roomIndex) => $"maxReachedLevel{roomIndex}";

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
            GameState.roomStates = new List<RoomState>();
            for (int roomIndex = 0; roomIndex < GameState.roomsAmount; roomIndex++)
            {
                var roomState = new RoomState();

                roomState.currentStyleIndex = new ReactiveProperty<int>
                    (data.ContainsKey(CurrentStyleIndexKey(roomIndex)) ?
                    int.Parse(data[CurrentStyleIndexKey(roomIndex)]) : 0);

                roomState.newStyleIndices = new ReactiveList<int>();
                if (data.ContainsKey(NewStyleIndicesKey(roomIndex)) && data[NewStyleIndicesKey(roomIndex)] != string.Empty)
                {
                    var splitData = data[NewStyleIndicesKey(roomIndex)].Split('_');
                    roomState.newStyleIndices.Add(int.Parse(splitData[0]));
                }  

                roomState.boxesAmount = new ReactiveDictionary<BoxType, int>();
                if (data.ContainsKey(BoxesAmountKey(roomIndex)) && data[BoxesAmountKey(roomIndex)] != string.Empty)
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

                roomState.placedItemsHierarchy = new ReactiveList<PlacedItemModel>();
                if (data.ContainsKey(ItemsHierarchyKey(roomIndex)) && data[ItemsHierarchyKey(roomIndex)] != string.Empty)
                {
                    var splitData = data[ItemsHierarchyKey(roomIndex)].Split(',');
                    foreach (var placedItemData in splitData)
                        roomState.placedItemsHierarchy.Add(new PlacedItemModel(placedItemData));
                }

                roomState.cards = new ReactiveList<CardModel>();
                if (data.ContainsKey(CardsKey(roomIndex)) && data[CardsKey(roomIndex)] != string.Empty)
                {
                    var splitData = data[CardsKey(roomIndex)].Split('_');
                    foreach (var cardData in splitData)
                        roomState.cards.Add(new CardModel(cardData));
                }

                roomState.size = new ReactiveProperty<Vector2Int>();
                if (data.ContainsKey(RoomSizeKey(roomIndex)))
                {
                    var splitSizeData = data[RoomSizeKey(roomIndex)].Split('_');
                    var size = new Vector2Int(int.Parse(splitSizeData[0]), int.Parse(splitSizeData[1]));

                    roomState.size = new ReactiveProperty<Vector2Int>(size);
                }
                else roomState.size = new ReactiveProperty<Vector2Int>(startValuesConfig.RoomSize);

                roomState.maxReachedLevel = data.ContainsKey(MaxReachedLevel(roomIndex)) ?
                    int.Parse(data[MaxReachedLevel(roomIndex)]) : 1;

                GameState.roomStates.Add(roomState);
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
                foreach (var boxAmount in roomState.boxesAmount)
                {
                    boxData += $"{(int)boxAmount.Key},{boxAmount.Value}";
                    if (index != roomState.boxesAmount.Count - 1) boxData += '_';
                    index++;
                }
                data.Add(BoxesAmountKey(roomIndex), boxData);

                data.Add(AccumulatedMoneyKey(roomIndex), roomState.accumulatedMoney.ToString());

                index = 0;
                var itemHierarchyData = string.Empty;
                foreach (var placedItem in roomState.placedItemsHierarchy)
                {
                    itemHierarchyData += placedItem.ToDataString();
                    if (index != roomState.placedItemsHierarchy.Count - 1) itemHierarchyData += ',';
                    index++;
                }
                data.Add(ItemsHierarchyKey(roomIndex), itemHierarchyData);

                index = 0;
                var cardsData = string.Empty;
                foreach (var cardModel in roomState.cards)
                {
                    cardsData += cardModel.ToDataString();
                    if (index != roomState.cards.Count - 1) cardsData += '_';
                    index++;
                }
                data.Add(CardsKey(roomIndex), cardsData);

                data.Add(RoomSizeKey(roomIndex), $"{roomState.size.Value.x}_{roomState.size.Value.y}");
                data.Add(MaxReachedLevel(roomIndex), roomState.maxReachedLevel.ToString());
            }




            return data;
        }


    }
}


