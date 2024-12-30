using System.Collections.Generic;
using UnityEngine;
using VG2;

public class UnboxingController
{
    private UnboxingView _unboxingView;
    private int _cardsAmount;
    private int _currentCardIndex;
    private UnboxingModel _unboxingModel;
    private bool _totalCardsShown = false;


    public void RunUnboxing(BoxType boxType, int cardsAmount)
    {
        _totalCardsShown = false;
        _cardsAmount = cardsAmount;
        _currentCardIndex = 0;

        _unboxingModel = GenerateUnboxingModel(boxType, cardsAmount);
        _unboxingView = SceneContainer.InstantiatePrefabFromComponent(ConfigHub.Unboxing.UnboxingWindowPrefab, UI.Canvas);
        _unboxingView.StartUnboxing(_unboxingModel);

    }

    public void Interact()
    {
        if (_unboxingView.Interactable == false) return;

        if (_currentCardIndex < _cardsAmount)
        {
            var cardModel = _unboxingModel.generatedCards[_currentCardIndex];
            _unboxingView.OpenNewCard(_unboxingModel, _currentCardIndex);
            CardCalculator.AddCard(cardModel);
            BoxCalculator.RemoveBoxes(_unboxingModel.boxType, 1);

            _currentCardIndex++;
        }
        else if (!_totalCardsShown)
        {
            _unboxingView.ShowTotalCards(_unboxingModel);
            _totalCardsShown = true;
        }
        else Object.Destroy(_unboxingView.gameObject);
    }

    public void Skip()
    {
        for (; _currentCardIndex < _cardsAmount; _currentCardIndex++)
        {
            var cardModel = _unboxingModel.generatedCards[_currentCardIndex];
            CardCalculator.AddCard(cardModel);
            BoxCalculator.RemoveBoxes(_unboxingModel.boxType, 1);
        }

        _unboxingView.ShowTotalCards(_unboxingModel);
        _totalCardsShown = true;
    }


    private UnboxingModel GenerateUnboxingModel(BoxType boxType, int cardsAmount)
    {
        UnboxingModel model = new UnboxingModel();
        model.boxType = boxType;
        model.generatedCards = new List<CardModel>(cardsAmount);

        var probabilities = ConfigHub.Unboxing.GetRarityProbabilites(boxType);

        for (int i = 0; i < cardsAmount; i++)
        {
            float randomValue = Random.Range(0f, 100f);
            float from = 0f;
            foreach (var probability in probabilities)
            {
                float to = from + probability.Value;

                if (from < randomValue && randomValue < to)
                {
                    model.generatedCards.Add(GetRandomCardModel(probability.Key));
                    break;
                }

                from = to;
            }
        }

        return model;
    }


    private CardModel GetRandomCardModel(RarityType rarityType)
    {
        var allRarityItems = Prefabs.GetAllRaritySortedItems()[rarityType];
        int randomIndex = Random.Range(0, allRarityItems.Count);

        var cardModel = allRarityItems[randomIndex].GetCardModel();
        cardModel.rarityType = rarityType;

        return cardModel;
    }




}
