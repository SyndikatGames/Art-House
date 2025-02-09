using TMPro;
using UnityEngine;
using VG2;

public class InAppPurchaseBoxPackWindowView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descriptionText;


    public void Display(ProductType boxPackProductKey)
    {
        string description = $"{Localization.GetString("inside_box_pack")}\n\n";

        foreach (var boxAmount in ConfigHub.Shop.GetBoxesFromBoxPack(boxPackProductKey))
        {
            string colorHtml = ColorUtility.ToHtmlStringRGB(ConfigHub.Unboxing.GetBoxTextColor(boxAmount.Key));
            string postfix = Localization.GetString($"of_boxes_{boxAmount.Key}");

            description += $"<color=#{colorHtml}>x{boxAmount.Value} {postfix}\n";
        }

        _descriptionText.text = description;
    }


}
