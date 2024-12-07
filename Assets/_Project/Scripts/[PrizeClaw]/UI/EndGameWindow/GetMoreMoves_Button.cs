using UnityEngine;
using VG;

namespace PrizeClaw
{
    public class GetMoreMoves_Button : ButtonHandler
    {
        [SerializeField] private EndGameWindow _window;


        protected override void OnClick()
        {
            Ads.Rewarded.Show(Key_Ad.prize_claw_continue, onShown: (result) =>
            {
                if (result == Ads.Rewarded.Result.Success)
                {
                    GameState.Current.AddMoves(3);
                    Destroy(_window.gameObject);
                }
            });

            

        }

    }
}
