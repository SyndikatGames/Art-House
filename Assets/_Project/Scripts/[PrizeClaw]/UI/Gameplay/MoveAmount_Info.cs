using VG;


namespace PrizeClaw
{
    public class MoveAmount_Info : Info
    {


        protected override void Subscribe()
        {
            GameState.onChanged += UpdateValue;
        }

        protected override void Unsubscribe()
        {
            GameState.onChanged -= UpdateValue;
        }

        protected override void UpdateValue()
        {
            text.text = GameState.Current.Moves.ToString();
        }

    }
}

