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

    public CardModel Data { get; private set; }

    [SerializeField] private Image _icon;
    [SerializeField] private Image _panel;
    [SerializeField] private Image _interactionImage;
    [SerializeField] private TextMeshProUGUI _amountText;

    public void DisableRaycast() => _interactionImage.raycastTarget = false;

    public void SetData(CardModel cardData)
    {
        var cardStyle = Configs.CardStyles.GetStyle(cardData.rarityType);
        Data = cardData;

        _amountText.gameObject.SetActive(cardData.amount > 1);
        _amountText.text = cardData.amount.ToString();

        _icon.sprite = Prefabs.GetItem(cardData.itemType).GetSprite(cardData.rarityType);
        _icon.transform.localScale = Vector3.one * CardStyle.GetCardScaleForBottomPanel(_icon.sprite);
        _panel.sprite = cardStyle.panelSprite;
    }

}

public static partial class Prefabs
{
    public static ItemCard ItemCard
        => Resources.Load<ItemCard>("Prefabs/ItemCard");
}
