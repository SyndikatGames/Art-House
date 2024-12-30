using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;
using R3;

public class BoxProductView : ReactiveView
{
    [SerializeField] private BoxType _boxType; public BoxType BoxType => _boxType;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private TextMeshProUGUI _amountText;

    [Header("Resources")]
    [SerializeField] private Sprite _freeButtonSprite;
    [SerializeField] private Sprite _paidButtonSprite;

    protected override void Subscribe()
    {
        disposables.Add(GameState.CurrentRoom.boxesAmount.onChanged.Subscribe(_ => Display()));
    }



    protected override void Display()
    {
        int currentBoxes = BoxCalculator.GetBoxesAmount(_boxType);
        bool paidBox = currentBoxes == 0;

        _amountText.gameObject.SetActive(!paidBox);
        _amountText.text = currentBoxes.ToString();

        _buttonImage.sprite = paidBox ? _paidButtonSprite : _freeButtonSprite;

        if (paidBox)
        {
            float boxPrice = ConfigHub.BaseValues.GetBoxPrice(_boxType);
            _buttonText.text = $"<sprite=0>{boxPrice.ToShortNumber()}";
        }
        else _buttonText.text = Localization.GetString("open");



    }

    
}
