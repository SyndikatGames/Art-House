using DG.Tweening;
using TMPro;
using UnityEngine;

public class SellParticle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sellPriceText;

    private const float moveY = 150f;
    private const float animDuration = 0.7f;


    public void RunAnimation()
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.DOMoveY(rectTransform.position.y + moveY, animDuration).SetEase(Ease.Linear);
        _sellPriceText.DOFade(0f, animDuration).SetEase(Ease.Linear)
            .onComplete += () => Destroy(gameObject);
    }


    public void SetSellPrice(float sellPrice)
        => _sellPriceText.text = $"<sprite=0> {sellPrice}";



}
