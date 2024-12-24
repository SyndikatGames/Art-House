using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;
using Zenject;


public class BoxWindowView : ReactiveView
{
    [Inject] private DesignConfig _designConfig;
    [Inject] private UnboxingConfig _unboxingConfig;


    [SerializeField] private BoxType _boxType; public BoxType BoxType => _boxType;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI _boxNameText;
    [SerializeField] private TextMeshProUGUI _probabilitiesText;
    [SerializeField] private Image _boxIcon;

    [SerializeField] private Button _groupOpenButton;
    [SerializeField] private TextMeshProUGUI _groupOpenButtonText;
    [SerializeField] private TextMeshProUGUI _singleOpenButtonText;

    //public BoxProductModel Model { get; private set; }

    protected override void Subscribe()
    {
        throw new System.NotImplementedException();
    }


    protected override void Display()
    {
        /*
        //Model = boxType;

        _boxIcon.sprite = _unboxingConfig.GetBoxSprite(boxType);
        _boxNameText.text = boxType.ToString();

        string probabilitiesText = "ÿ‡ÌÒ˚:\n\n";
        foreach (var rarityProbability in _unboxingConfig.GetRarityProbabilites(boxType))
        {
            var probabilityColorHtml = ColorUtility.ToHtmlStringRGB
                (_unboxingConfig.GetRarityStyle(rarityProbability.Key).textColor);

            probabilitiesText += $"<color=#{probabilityColorHtml}>{rarityProbability.Key}: " +
                $"{rarityProbability.Value.ToString("#.##")}%\n";
        }

        _probabilitiesText.text = probabilitiesText;

        if (boxType.currentBoxAmount > 1)
        {
            _groupOpenButton.gameObject.SetActive(true);

            _singleOpenButtonText.text = "Œ“ –€“‹ ı1";
            _groupOpenButtonText.text = $"Œ“ –€“‹ ı{boxType.currentBoxAmount}";
        }
        else if (boxType.currentBoxAmount == 1)
        {
            _groupOpenButton.gameObject.SetActive(false);
            _singleOpenButtonText.text = "Œ“ –€“‹ ı1";
        }
        else
        {
            _groupOpenButton.gameObject.SetActive(true);

            _singleOpenButtonText.text = $" ”œ»“‹ ı1\n<sprite=0>{boxType.singlePrice.ToShortNumber()}";
            _groupOpenButtonText.text = $"Œ“ –€“‹ ı{boxType.boxesInGroupAmount}\n" +
                $"<sprite=0>{boxType.groupPrice.ToShortNumber()}";
        }
        */
    }
}
