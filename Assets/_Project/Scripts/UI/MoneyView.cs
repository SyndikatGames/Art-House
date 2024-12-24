using R3;
using TMPro;
using UnityEngine;
using VG2;

public class MoneyView : ReactiveView
{
    [SerializeField] private TextMeshProUGUI _text;

    protected override void Subscribe()
    {
        disposables.Add(GameState.money.Subscribe(_ => Display()));
    }

    protected override void Display()
    {
        _text.text = GameState.money.Value.ToShortNumber();
    }
    
}