using UnityEngine;
using VG2;
using R3;

public class InAppPurchaseBoxProductViewModification : ReactiveView
{
    [SerializeField] private GameObject _openButton;
    [SerializeField] private GameObject _inAppPurchaseButton;


    protected override void Subscribe()
    {
        disposables.Add(GameState.CurrentRoom.boxesAmount.OnChanged.Subscribe(_ => Display()));
    }

    protected override void Display()
    {
        bool hasBoxes = BoxCalculator.GetBoxesAmount(BoxType.Fantastic) > 0;

        _openButton.SetActive(hasBoxes);
        _inAppPurchaseButton.SetActive(!hasBoxes);
    }

    
}
