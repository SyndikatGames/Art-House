using DG.Tweening;
using TMPro;
using UnityEngine;

public class SellParticle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sellPriceText;

    private const float moveY = 1f;
    private const float animDuration = 0.7f;

    private void OnEnable()
    {
        transform.DOMoveY(transform.position.y + moveY, animDuration).SetEase(Ease.Linear);
        _sellPriceText.DOFade(0f, animDuration).SetEase(Ease.Linear)
            .onComplete += () => Destroy(gameObject);
    }



    public void SetSellPrice(int sellPrice)
        => _sellPriceText.text = $"<sprite=0> {sellPrice}";



}
