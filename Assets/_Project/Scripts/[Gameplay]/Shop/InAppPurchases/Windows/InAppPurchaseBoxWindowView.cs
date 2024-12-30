using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;

public class InAppPurchaseBoxWindowView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _boxNameText;
    [SerializeField] private TextMeshProUGUI _probabilitiesText;
    [SerializeField] private Image _boxIcon;

    [SerializeField] private PurchaseButton _purchaseButton;
    [SerializeField] private PurchasePrice _purchasePrice;


    public void Display(BoxType boxType, ProductKey boxProductKey)
    {
        _boxIcon.sprite = ConfigHub.Unboxing.GetBoxSprite(boxType);
        _boxNameText.text = Localization.GetString($"box_window_{boxType}");

        string probabilitiesText = $"{Localization.GetString("item_probabilities")}\n\n";
        foreach (var rarityProbability in ConfigHub.Unboxing.GetRarityProbabilites(boxType))
        {
            var probabilityColorHtml = ColorUtility.ToHtmlStringRGB
                (ConfigHub.Cards.GetRarityStyle(rarityProbability.Key).textColor);

            string itemRarityName = Localization.GetString($"item_{rarityProbability.Key}");

            probabilitiesText += $"<color=#{probabilityColorHtml}>{itemRarityName}: " +
                $"{rarityProbability.Value.ToString("#.##")}%\n";
        }

        _probabilitiesText.text = probabilitiesText;

        _purchaseButton.SetProduct(boxProductKey);
        _purchasePrice.SetProduct(boxProductKey);
    }



}
