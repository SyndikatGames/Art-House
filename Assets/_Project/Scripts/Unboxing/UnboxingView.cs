using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnboxingView : MonoBehaviour
{
    [System.Serializable]
    private struct BoxSpriteData
    {
        public BoxType boxType;
        public Sprite boxSprite;
        public Sprite openedBoxSprite;
    }

    [Header("Opening Cards:")]
    [SerializeField] private GameObject _skipButton;
    [SerializeField] private RectTransform _spawnCardPoint;
    [SerializeField] private GameObject _prompt;
    [SerializeField] private GameObject _counter;
    [SerializeField] private RectTransform _targetCardPoint;
    [SerializeField] private RectTransform _boxRect;
    [SerializeField] private RectTransform _shineRect;
    [SerializeField] private Image _boxImage;
    [SerializeField] private TextMeshProUGUI _counterText;

    [Header("Total Cards:")]
    [SerializeField] private RectTransform _totalCardsRect;
    [SerializeField] private Transform _totalCardsContainer;
    [SerializeField] private TextMeshProUGUI _moreCardsText;

    [Header("Resources:")]
    [SerializeField] private UnboxedCardView _unboxedCardViewPrefab;
    [SerializeField] private List<BoxSpriteData> _boxSprites;

    public bool Interactable { get; private set; } = true;

    private UnboxedCardView _unboxedCardView;
    private RectTransform _unboxedCardRect;
    private BoxSpriteData _boxSpriteData;
    private bool _boxOpened;

    private const int showOpenAllButtonIfBoxesMore = 4;
    private const float shineMaxScale = 1.8f;
    private const float showBoxDuration = 1f;
    private const float showNewCardDuration = 0.8f;
    private const float openedBoxScale = 0.8f;
    private const float moveBoxY = -200f;
    private const float moveCardY = 100f;
    private const int maxUnboxedCards = 10;
    private const float showTotalCardsDuration = 1f;

    public void StartUnboxing(UnboxingModel unboxingModel)
    {
        _skipButton.SetActive(unboxingModel.generatedCards.Count > showOpenAllButtonIfBoxesMore);
        _shineRect.gameObject.SetActive(false);

        _boxSpriteData = _boxSprites.Find(boxSprite => boxSprite.boxType == unboxingModel.boxType);
        _boxImage.sprite = _boxSpriteData.boxSprite;

        _counterText.text = unboxingModel.generatedCards.Count.ToString();

        _boxRect.localScale = Vector3.zero;

        Interactable = false;
        _boxRect.DOScale(1f, showBoxDuration).SetEase(Ease.OutBack)
            .onComplete += () => Interactable = true;

        _prompt.SetActive(true);
        _boxOpened = false;
    }

    public void OpenNewCard(UnboxingModel unboxingModel, int cardIndex)
    {
        if (!_boxOpened)
        {
            _prompt.SetActive(false);
            _boxImage.sprite = _boxSpriteData.openedBoxSprite;

            _boxRect.localScale = Vector3.one;

            _boxRect.DOMoveY(_boxRect.position.y + moveBoxY, showNewCardDuration).SetEase(Ease.OutFlash);
            _boxRect.DOScale(openedBoxScale, showNewCardDuration).SetEase(Ease.OutFlash);

            _boxOpened = true;
        }

        if (_unboxedCardView != null) Destroy(_unboxedCardView.gameObject);
        _unboxedCardView = Instantiate(_unboxedCardViewPrefab, UI.Canvas);
        _unboxedCardRect = _unboxedCardView.GetComponent<RectTransform>();

        _unboxedCardView.Display(unboxingModel.generatedCards[cardIndex]);

        int boxesLeft = unboxingModel.generatedCards.Count - cardIndex - 1;

        _counter.SetActive(boxesLeft > 0);
        _counterText.text = boxesLeft.ToString();

        _unboxedCardRect.position = _spawnCardPoint.position;
        _unboxedCardRect.localScale = Vector3.zero;

        _unboxedCardRect.DOMoveY(_targetCardPoint.position.y, showNewCardDuration).SetEase(Ease.OutFlash);
        _unboxedCardRect.DOScale(1f, showNewCardDuration).SetEase(Ease.OutFlash)
            .onComplete += () => _unboxedCardView.DisplayInfo();

        Interactable = false;
        _unboxedCardView.onAnimationFinished += OnBoxCardAnimationFinished;

        _shineRect.gameObject.SetActive(true);
        _shineRect.localScale = Vector3.one;
        DOTween.Sequence(transform)
            .Append(_shineRect.DOScale(shineMaxScale, showNewCardDuration / 2f).SetEase(Ease.OutFlash))
            .Append(_shineRect.DOScale(1f, showNewCardDuration / 3f).SetEase(Ease.InFlash));

    }

    private void OnBoxCardAnimationFinished()
    {
        _unboxedCardView.onAnimationFinished -= OnBoxCardAnimationFinished;
        Interactable = true;
    }

    public void ShowTotalCards(UnboxingModel unboxingModel)
    {
        if (_unboxedCardRect != null)
        {
            _unboxedCardRect.DOKill();
            Destroy(_unboxedCardRect.gameObject);
        }
        
        _totalCardsRect.gameObject.SetActive(true);
        _totalCardsRect.localScale = Vector3.zero;

        Interactable = false;
        _boxRect.DOScale(0f, showTotalCardsDuration).SetEase(Ease.OutQuad);
        _totalCardsRect.DOScale(1f, showTotalCardsDuration).SetEase(Ease.OutBack)
            .onComplete += () => Interactable = true;

        _skipButton.SetActive(false);


        foreach (Transform child in _totalCardsContainer)
            Destroy(child.gameObject);

        var cardModels = new List<CardModel>();
        for (int i = 0; i < unboxingModel.generatedCards.Count; i++)
        {
            var cardModel = unboxingModel.generatedCards[i];

            bool cardExists = false;
            for (int j = 0; j < cardModels.Count; j++)
                if (cardModels[j].itemType == cardModel.itemType 
                    && cardModels[j].rarityType == cardModel.rarityType)
                {
                    cardModels[j] = new CardModel
                    {
                        itemType = cardModels[j].itemType,
                        rarityType = cardModels[j].rarityType,
                        amount = cardModels[j].amount + 1,
                    };

                    cardExists = true;
                    break;
                }

            if (!cardExists) cardModels.Add(cardModel);
        }

        int nonShowedCard = unboxingModel.generatedCards.Count;
        for (int i = 0; i < cardModels.Count && i < maxUnboxedCards; i++)
        {
            Instantiate(_unboxedCardViewPrefab, _totalCardsContainer).Display(cardModels[i]);
            nonShowedCard -= cardModels[i].amount;
        }

        _moreCardsText.gameObject.SetActive(nonShowedCard > 0);
        _moreCardsText.text = nonShowedCard.ToString();


    }


}

public static partial class Prefabs
{
    public static UnboxingView BoxOpeningWindow
        => Resources.Load<UnboxingView>("Windows/BoxOpening");
}
