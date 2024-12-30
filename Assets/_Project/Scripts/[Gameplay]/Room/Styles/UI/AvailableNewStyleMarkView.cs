using UnityEngine;
using VG2;
using R3;

public class AvailableNewStyleMarkView : ReactiveView
{
    [SerializeField] private GameObject _mark;


    protected override void Subscribe()
    {
        disposables.Add(GameState.CurrentRoom.newStyleIndices.onChanged.Subscribe(_ => Display()));
    }


    protected override void Display()
    {
        bool hasNewStyles = GameState.CurrentRoom.newStyleIndices.Count > 0;
        _mark.SetActive(hasNewStyles);
    }
}
