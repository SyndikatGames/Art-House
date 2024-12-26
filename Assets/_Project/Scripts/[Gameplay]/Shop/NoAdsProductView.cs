using UnityEngine;
using VG2;
using R3;

public class NoAdsProductView : ReactiveView
{
    [SerializeField] private GameObject _purchaseButton;
    [SerializeField] private GameObject _purchasedLabel;

    protected override void Subscribe()
    {
        disposables.Add(GameState.adsEnabled.Subscribe(_ => Display()));
    }

    protected override void Display()
    {
        _purchaseButton.SetActive(!GameState.adsEnabled.Value);
        _purchasedLabel.SetActive(GameState.adsEnabled.Value);
    }

    
}
