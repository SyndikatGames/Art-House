using UnityEngine;
using VG2;
using R3;


public class ShopActionAvailableMarkerView : ReactiveView
{
    [SerializeField] private GameObject _mark;


    protected override void Subscribe()
    {
        disposables.Add(GameState.CurrentRoom.boxesAmount.OnChanged.Subscribe(_ => Display()));
    }


    protected override void Display()
    {
        _mark.SetActive(ShopCalculator.ShopActionAvailable);
    }




}
