using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace VG
{

    public partial class Saves
    {

        public static List<CardData> GetCards()
        {
            var result = new List<CardData>();
            if (String[Key_Save.cards_data(0)].Value == string.Empty) 
                return result;


            var splitData = String[Key_Save.cards_data(0)].Value.Split(',');

            foreach (var cardData in splitData)
            {
                var splitCardData = cardData.Split('_');
                result.Add(new CardData
                {
                    itemType = (ItemType)int.Parse(splitCardData[0]),
                    rarityType = (RarityType)int.Parse(splitCardData[1]),
                    amount = int.Parse(splitCardData[2]),
                });
            }

            return result;
        }

        public static void RemoveCard(CardData cardData)
        {
            var cardDataList = GetCards();

            for (int i = 0; i < cardDataList.Count; i++)
            {
                var data = cardDataList[i];
                if (cardData.itemType == data.itemType && cardData.rarityType == data.rarityType)
                {
                    data.amount = Mathf.Max(0, data.amount - cardData.amount);
                    if (data.amount != 0) cardDataList[i] = data;
                    else cardDataList.RemoveAt(i);

                    break;
                }
            }

            SetCards(cardDataList);
        }

        public static void AddCard(CardData cardData)
        {
            var cardDataList = GetCards();

            bool cardExists = false;
            for (int i = 0; i < cardDataList.Count; i++)
            {
                var data = cardDataList[i];
                if (cardData.itemType == data.itemType && cardData.rarityType == data.rarityType)
                {
                    data.amount += cardData.amount;
                    cardDataList[i] = data;
                    cardExists = true;
                    break;
                }
            }

            if (!cardExists)
                cardDataList.Add(cardData);

            SetCards(cardDataList);
        }

        public static void UpdateCards() 
            => String[Key_Save.cards_data(0)].Value = String[Key_Save.cards_data(0)].Value;

        private static void SetCards(List<CardData> cardDataList)
        {
            string resultData = string.Empty;

            for (int i = 0; i < cardDataList.Count; i++)
            {
                var cardData = cardDataList[i];

                resultData += $"{(int)cardData.itemType}_{(int)cardData.rarityType}_{cardData.amount}";
                if (i != cardDataList.Count - 1) resultData += ',';
            }   

            String[Key_Save.cards_data(0)].Value = resultData;
        }


    }


}