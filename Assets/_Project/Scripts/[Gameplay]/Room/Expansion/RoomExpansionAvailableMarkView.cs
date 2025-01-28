using R3;
using UnityEngine;
using VG2;

public class RoomExpansionAvailableMarkView : ReactiveView
{
    [SerializeField] private GameObject _mark;



    protected override void Subscribe()
    {
        disposables.Add(GameState.money.Subscribe(_ => Display()));
        disposables.Add(GameState.CurrentRoom.expansionLevel.Subscribe(_ => Display()));
    }


    protected override void Display()
    {
        float price = ConfigHub.BaseValues
            .GetRoomExpansionPrice(GameState.CurrentRoom.expansionLevel.Value);

        _mark.SetActive(GameState.money.Value >= price);
    }

    
}
