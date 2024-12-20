using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG;

public class BoxOpeningWindow : MonoBehaviour
{
    [System.Serializable]
    private struct BoxSpriteData
    {
        public RarityType rarityType;
        public Sprite boxSprite;
        public Sprite openedBoxSprite;
    }

    [SerializeField] private GameObject _openAllButton;
    [SerializeField] private BoxCardView _boxCard;
    [SerializeField] private RectTransform _spawnPoint;
    [SerializeField] private GameObject _prompt;
    [SerializeField] private GameObject _counter;
    [SerializeField] private RectTransform _targetPoint;
    [SerializeField] private RectTransform _boxRect;
    [SerializeField] private RectTransform _shineRect;
    [SerializeField] private Image _boxImage;
    [SerializeField] private TextMeshProUGUI _counterText;
    [SerializeField] private List<BoxSpriteData> _boxSprites;

    private Tween _boxTween;
    private BoxSpriteData _boxSpriteData;
    private int _currentCardIndex;
    private List<CardModel> _cardDataList;
    private RectTransform _boxCardRect;

    private const int showOpenAllButtonIfBoxesMore = 4;
    private const float shineMaxScale = 1.8f;
    private const float showBoxDuration = 0.7f;
    private const float openBoxDuration = 0.9f;
    private const float openedBoxScale = 0.8f;
    private const float moveBoxY = -300f;
    private const float moveCardY = 100f;

    private bool _animationFinished = false;

    public void Display(BoxOpeningModel boxOpeningModel)
    {
        _openAllButton.SetActive(_cardDataList.Count > showOpenAllButtonIfBoxesMore);

        _shineRect.gameObject.SetActive(false);
        _cardDataList = boxOpeningModel.generatedCards;

        _currentCardIndex = 0;
        _boxSpriteData = _boxSprites.Find(boxSprite => boxSprite.rarityType == boxOpeningModel.boxRarityType);
        _boxImage.sprite = _boxSpriteData.boxSprite;

        _counterText.text = _cardDataList.Count.ToString();

        _boxRect.localScale = Vector3.zero;

        _animationFinished = false;
        _boxRect.DOScale(1f, showBoxDuration).SetEase(Ease.OutBack)
            .onComplete += () => _animationFinished = true;

        _boxCard.gameObject.SetActive(false);

        _boxCardRect = _boxCard.GetComponent<RectTransform>();
        _prompt.SetActive(true);
    }


    public void Interact() // [Button]
    {
        if (!_animationFinished) return;

        if (_currentCardIndex < _cardDataList.Count)
            OpenNewCard();

        else Destroy(gameObject);
    }

    private void OpenNewCard()
    {
        var cardData = _cardDataList[_currentCardIndex];

        if (_currentCardIndex == 0)
        {
            _boxCard.gameObject.SetActive(true);
            _prompt.SetActive(false);
            _boxImage.sprite = _boxSpriteData.openedBoxSprite;

            _boxRect.localScale = Vector3.one;
            _boxTween?.Kill();

            _boxRect.DOMoveY(_boxRect.position.y + moveBoxY, openBoxDuration).SetEase(Ease.OutFlash);
            _boxRect.DOScale(openedBoxScale, openBoxDuration).SetEase(Ease.OutFlash);

        }

        
        _boxCard.Display(cardData);

        int boxesLeft = _cardDataList.Count - _currentCardIndex - 1;

        _counter.SetActive(boxesLeft > 0);
        _counterText.text = boxesLeft.ToString();

        _boxCardRect.position = _spawnPoint.position;
        _boxCardRect.localScale = Vector3.zero;

        _boxCardRect.DOMoveY(_targetPoint.position.y, openBoxDuration).SetEase(Ease.OutFlash);
        _boxCardRect.DOScale(1f, openBoxDuration).SetEase(Ease.OutFlash)
            .onComplete += () => _boxCard.DisplayInfo();

        _animationFinished = false;
        _boxCard.onAnimationFinished += OnBoxCardAnimationFinished;

        _shineRect.gameObject.SetActive(true);
        _shineRect.localScale = Vector3.one;
        DOTween.Sequence(transform)
            .Append(_shineRect.DOScale(shineMaxScale, openBoxDuration / 2f).SetEase(Ease.OutFlash))
            .Append(_shineRect.DOScale(1f, openBoxDuration / 3f).SetEase(Ease.InFlash));

        _currentCardIndex++;
        Saves.AddCard(cardData);
    }

    private void OnBoxCardAnimationFinished()
    {
        _boxCard.onAnimationFinished -= OnBoxCardAnimationFinished;
        _animationFinished = true;
    }

    public void OpenAll() // [Button]
    {


    }


}

public static partial class Prefabs
{
    public static BoxOpeningWindow BoxOpeningWindow
        => Resources.Load<BoxOpeningWindow>("Windows/BoxOpening");
}
