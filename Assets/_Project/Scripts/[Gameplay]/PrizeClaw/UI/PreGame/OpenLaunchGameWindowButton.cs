using VG2;
using Zenject;


namespace PrizeClaw
{
    public class OpenLaunchGameWindowButton : ButtonHandler
    {

        [Inject] private GameController _prizeClawController;


        protected override void OnClick()
        {
            _prizeClawController.OpenLaunchGameWindow();
        }

    }
}
