using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnboxedCardView : MonoBehaviour
{
    public event Action onAnimationFinished;


    [SerializeField] private Image _itemIcon;
    [SerializeField] private Image _panelImage;
    [SerializeField] private RectTransform _panelRect;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _amountText;

    private RectTransform _descriptionRect;
    private CardModel _cardModel;

    private const float panelMoveX = -180f;
    private const float infoMoveX = 20f;
    private const float showDuration = 0.8f;


    public void Display(CardModel cardModel)
    {
        _descriptionRect = _descriptionText.GetComponent<RectTransform>();

        _cardModel = cardModel;
        _panelRect.localPosition = Vector3.zero;
        _descriptionRect.localPosition = Vector3.zero;
        _descriptionRect.gameObject.SetActive(false);

        var cardStyle = Configs.CardStyles.GetStyle(cardModel.rarityType);

        _itemIcon.sprite = Prefabs.GetItem(cardModel.itemType).GetSprite(cardModel.rarityType);
        _itemIcon.transform.localScale = Vector3.one * CardStyle.GetCardScaleForBoxOpening(_itemIcon.sprite);
        _panelImage.sprite = cardStyle.panelSprite;

        int prestigePoints = TotalRules.GetItemPrestige(cardModel.rarityType);

        string rarityColor = ColorUtility.ToHtmlStringRGB(cardStyle.textColor);
        _descriptionText.text = $"{_cardModel.itemType}\n" +
            $"<size=46><color=#{rarityColor}>{_cardModel.rarityType}</color>\n\n" +
            $"<color=yellow>+{prestigePoints}</color>";

        _amountText.gameObject.SetActive(cardModel.amount > 1);
        _amountText.text = cardModel.amount.ToString();
    }

    public void DisplayInfo()
    {
        _panelRect.DOLocalMoveX(panelMoveX, showDuration).SetEase(Ease.OutFlash);
        _descriptionRect.gameObject.SetActive(true);
        _descriptionRect.DOLocalMoveX(infoMoveX, showDuration).SetEase(Ease.OutFlash)
            .onComplete += () => onAnimationFinished?.Invoke();
        
    }



}
