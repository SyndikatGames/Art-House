using UnityEngine;
using VG2;

public static class CardCalculator
{
    
    public static void AddCard(CardModel cardModel)
    {
        for (int i = 0; i < GameState.CurrentRoom.cards.Count; i++)
        {
            var card = GameState.CurrentRoom.cards.Get(i);
            if (card.itemType == cardModel.itemType && card.rarityType == cardModel.rarityType)
            {
                var newCardModel = card;
                newCardModel.amount += cardModel.amount;
                GameState.CurrentRoom.cards.Set(i, newCardModel);
                return;
            }   
        }

        GameState.CurrentRoom.cards.Add(cardModel);
    }

    public static void RemoveCard(CardModel cardModel)
    {
        for (int i = 0; i < GameState.CurrentRoom.cards.Count; i++)
        {
            var card = GameState.CurrentRoom.cards.Get(i);
            if (card.itemType == cardModel.itemType && card.rarityType == cardModel.rarityType)
            {
                var newCardModel = card;
                newCardModel.amount = Mathf.Max(0, card.amount - cardModel.amount);

                if (newCardModel.amount > 0)
                    GameState.CurrentRoom.cards.Set(i, newCardModel);

                else GameState.CurrentRoom.cards.Remove(card);

                return;
            }
        }
    }


}
