using TMPro;
using UnityEngine;
using VG2;

public class InAppPurchaseView : MonoBehaviour
{
    [field: SerializeField] public ProductType ProductKey { get; private set; }

    [SerializeField] private TextMeshProUGUI _priceText;



    private void OnEnable()
    {
        _priceText.text = Purchases.GetPriceString(ProductKey);
    }


}
