using TMPro;
using UnityEngine;
using VG;


namespace PrizeClaw
{
    public class MoveAmount_Info : ReactiveView
    {
        [SerializeField] private TextMeshProUGUI _text;

        protected override void Subscribe()
        {
            GameState.onChanged += Display;
        }

        protected override void Dispose()
        {
            GameState.onChanged -= Display;
        }

        protected override void Display()
        {
            _text.text = GameState.Current.Moves.ToString();
        }

    }
}

