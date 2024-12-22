using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG;

public class BoxProductView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _purchaseText;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private GameObject _counter;

    [Header("Resources")]
    [SerializeField] private Sprite _freeButtonSprite;
    [SerializeField] private Sprite _paidButtonSprite;

    public BoxType BoxType { get; private set; }

    public void Display(ShopConfig shopConfig, BoxType boxType)
    {
        BoxType = boxType;

        int currentBoxes = Saves.GetBoxes(boxType);
        bool paidBox = currentBoxes == 0;

        _counter.SetActive(!paidBox);
        _amountText.text = currentBoxes.ToString();

        _buttonImage.sprite = paidBox ? _paidButtonSprite : _freeButtonSprite;

        float boxPrice = shopConfig.GetBoxPrice(boxType);
        _purchaseText.text = paidBox ? $"<sprite=0>{boxPrice.ToShortNumber()}" : "Открыть";
    }




}
