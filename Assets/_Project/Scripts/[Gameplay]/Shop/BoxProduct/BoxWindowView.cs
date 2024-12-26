using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;


public class BoxWindowView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _boxNameText;
    [SerializeField] private TextMeshProUGUI _probabilitiesText;
    [SerializeField] private Image _boxIcon;

    [SerializeField] private GameObject _groupOpenButton;
    [SerializeField] private TextMeshProUGUI _groupOpenButtonText;
    [SerializeField] private TextMeshProUGUI _singleOpenButtonText;
    [SerializeField] private float _originMaxTextSize;
    [SerializeField] private float _priceMaxTextSize;

    [Header("Resources")]
    [SerializeField] private Image _paidButtonSprite;
    [SerializeField] private Image _freeButtonSprite;

    public BoxType BoxType { get; private set; }


    public void Display(BoxType boxType)
    {
        BoxType = boxType;

        _boxIcon.sprite = ConfigHub.Unboxing.GetBoxSprite(boxType);
        _boxNameText.text = boxType.ToString();

        string probabilitiesText = "ÿ‡ÌÒ˚:\n\n";
        foreach (var rarityProbability in ConfigHub.Unboxing.GetRarityProbabilites(boxType))
        {
            var probabilityColorHtml = ColorUtility.ToHtmlStringRGB
                (ConfigHub.Cards.GetRarityStyle(rarityProbability.Key).textColor);

            probabilitiesText += $"<color=#{probabilityColorHtml}>{rarityProbability.Key}: " +
                $"{rarityProbability.Value.ToString("#.##")}%\n";
        }

        _probabilitiesText.text = probabilitiesText;
        _singleOpenButtonText.fontSizeMax = _originMaxTextSize;
        _groupOpenButtonText.fontSizeMax = _originMaxTextSize;

        if (BoxCalculator.GetBoxesAmount(boxType) > 1)
        {
            _groupOpenButton.SetActive(true);

            _singleOpenButtonText.text = "Œ“ –€“‹ ı1";
            _groupOpenButtonText.text = $"Œ“ –€“‹ ı{BoxCalculator.GetBoxesAmount(boxType)}";
        }
        else if (BoxCalculator.GetBoxesAmount(boxType) == 1)
        {
            _groupOpenButton.SetActive(false);
            _singleOpenButtonText.text = "Œ“ –€“‹ ı1";
        }
        else
        {
            _groupOpenButton.SetActive(true);

            float price = ConfigHub.BaseValues.GetBoxPrice(boxType);
            float groupPrice = price * ConfigHub.Shop.BoxesInGroup;

            _singleOpenButtonText.text = $" ”œ»“‹ ı1\n<sprite=0>{price}";
            _groupOpenButtonText.text = $" ”œ»“‹ ı{ConfigHub.Shop.BoxesInGroup}\n" +
                $"<sprite=0>{groupPrice.ToShortNumber()}";

            _singleOpenButtonText.fontSizeMax = _priceMaxTextSize;
            _groupOpenButtonText.fontSizeMax = _priceMaxTextSize;

        }
    }

}
