using UnityEngine;
using VG2;

namespace PrizeClaw
{
    public class GetMoreMoves_Button : ButtonHandler
    {
        [SerializeField] private EndGameWindowView _window;


        protected override void OnClick()
        {
            Ads.Rewarded.Show(Key_Ad.prize_claw_continue, onShown: (result) =>
            {
                if (result == Ads.Rewarded.Result.Success)
                {
                    GameState.prizeClaw.movesLeft.Value += 3;
                    Destroy(_window.gameObject);
                }
            });

            

        }

    }
}
