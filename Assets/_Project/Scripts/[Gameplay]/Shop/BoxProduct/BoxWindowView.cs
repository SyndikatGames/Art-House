using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;


public class BoxWindowView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _boxNameText;
    [SerializeField] private TextMeshProUGUI _probabilitiesText;
    [SerializeField] private Image _boxIcon;

    [field: SerializeField] public RectTransform OpenButtonRect { get; private set; }
    [SerializeField] private GameObject _groupOpenButton;
    [SerializeField] private TextMeshProUGUI _groupOpenButtonText;
    [SerializeField] private TextMeshProUGUI _singleOpenButtonText;
    [SerializeField] private float _originMaxTextSize;
    [SerializeField] private float _priceMaxTextSize;

    private string BuyText => Localization.GetString("buy");
    private string OpenText => Localization.GetString("open");


    public Image OpenButtonImage => OpenButtonRect.GetComponent<Image>();

    public BoxType BoxType { get; private set; }


    public void Display(BoxType boxType)
    {
        BoxType = boxType;

        _boxIcon.sprite = ConfigHub.Unboxing.GetBoxSprite(boxType);
        _boxNameText.text = Localization.GetString($"box_window_{boxType}");

        string probabilitiesText = $"{Localization.GetString("item_probabilities")}\n\n";
        foreach (var rarityProbability in ConfigHub.Unboxing.GetRarityProbabilites(boxType))
        {
            var probabilityColorHtml = ColorUtility.ToHtmlStringRGB
                (ConfigHub.Cards.GetRarityStyle(rarityProbability.Key).textColor);

            string itemRarityName = Localization.GetString($"{rarityProbability.Key}");

            probabilitiesText += $"<color=#{probabilityColorHtml}>{itemRarityName}: " +
                $"{rarityProbability.Value.ToString("#.##")}%\n";
        }

        _probabilitiesText.text = probabilitiesText;
        _singleOpenButtonText.fontSizeMax = _originMaxTextSize;
        _groupOpenButtonText.fontSizeMax = _originMaxTextSize;

        if (BoxCalculator.GetBoxesAmount(boxType) > 1)
        {
            _groupOpenButton.SetActive(true);

            _singleOpenButtonText.text =  $"{OpenText} õ1";
            _groupOpenButtonText.text = $"{OpenText} õ{BoxCalculator.GetBoxesAmount(boxType)}";
        }
        else if (BoxCalculator.GetBoxesAmount(boxType) == 1)
        {
            _groupOpenButton.SetActive(false);
            _singleOpenButtonText.text = $"{OpenText} õ1";
        }
        else
        {
            _groupOpenButton.SetActive(true);

            float price = ConfigHub.BaseValues.GetBoxPrice(boxType);
            float groupPrice = price * ConfigHub.Shop.BoxesInGroup;

            _singleOpenButtonText.text = $"{BuyText} õ1\n<sprite=0>{price}";
            _groupOpenButtonText.text = $"{BuyText} õ{ConfigHub.Shop.BoxesInGroup}\n" +
                $"<sprite=0>{groupPrice.ToShortNumber()}";

            _singleOpenButtonText.fontSizeMax = _priceMaxTextSize;
            _groupOpenButtonText.fontSizeMax = _priceMaxTextSize;

        }
    }

}
