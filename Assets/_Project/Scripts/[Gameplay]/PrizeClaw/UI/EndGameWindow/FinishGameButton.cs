using VG2;
using Zenject;

namespace PrizeClaw
{
    public class FinishGameButton : ButtonHandler
    {
        [Inject] private GameController _gameController;



        protected override void OnClick()
        {
            _gameController.FinishGame();
        }
    }
}
