using TMPro;
using UnityEngine;
using VG2;
using R3;


namespace PrizeClaw
{
    public class MoveAmount_Info : ReactiveView
    {
        [SerializeField] private TextMeshProUGUI _text;

        protected override void Subscribe()
        {
            disposables.Add(GameState.prizeClaw.movesLeft.Subscribe(_ => Display()));
        }

        protected override void Display()
        {
            _text.text = GameState.prizeClaw.movesLeft.Value.ToString();
        }

    }
}

