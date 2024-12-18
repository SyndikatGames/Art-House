using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [System.Serializable]
    private struct RarityPanelSprite
    {
        public RarityType rarityType;
        public Sprite sprite;
    }

    public CardData Data { get; private set; }

    [SerializeField] private Image _icon;
    [SerializeField] private Image _panel;
    [SerializeField] private Image _interactionImage;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private List<RarityPanelSprite> _panelSprites;

    public void DisableRaycast() => _interactionImage.raycastTarget = false;

    public void SetData(CardData cardData)
    {
        Data = cardData;

        _amountText.gameObject.SetActive(cardData.amount > 1);
        _amountText.text = cardData.amount.ToString();

        _icon.sprite = Prefabs.GetItem(cardData.itemType).GetSprite(cardData.rarityType);
        _icon.transform.localScale = Vector3.one * GetIconScale(_icon.sprite);
        _panel.sprite = _panelSprites.Find(raritySprite =>  raritySprite.rarityType == cardData.rarityType).sprite;
    }


    private float GetIconScale(Sprite sprite)
    {
        float maxSide = Mathf.Max(sprite.rect.width, sprite.rect.height);
        if (maxSide < 150f) return 0.7f;
        else if (maxSide < 250f) return 0.8f;
        else if (maxSide < 350f) return 0.9f;
        return 0.95f;
    }



}

public static partial class Prefabs
{
    public static ItemCard ItemCard
        => Resources.Load<ItemCard>("Prefabs/ItemCard");
}
