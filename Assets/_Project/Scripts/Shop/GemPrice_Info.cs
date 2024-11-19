using TMPro;
using UnityEngine;

public class GemPrice_Info : MonoBehaviour
{
    [SerializeField] private GemProduct _product;
    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _text ??= GetComponent<TextMeshProUGUI>();
        _text.text = $"<sprite=0> {Configs.Shop.GetGemProductPrice(_product.ProductType)}";
    }


}
