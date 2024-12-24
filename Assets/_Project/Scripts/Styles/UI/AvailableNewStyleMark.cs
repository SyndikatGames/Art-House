using UnityEngine;
using VG2;
using R3;

public class AvailableNewStyleMark : ReactiveView
{
    [SerializeField] private GameObject _mark;


    protected override void Subscribe()
    {
        disposables.Add(GameState.CurrentRoom.currentStyleIndex.Subscribe(_ => Display()));
    }


    protected override void Display()
    {
        bool hasNewStyles = GameState.CurrentRoom.newStyleIndices.Count > 0;
        _mark.SetActive(hasNewStyles);
    }
}
