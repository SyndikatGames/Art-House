using VG;


namespace PrizeClaw
{
    public class OpenPreGameWindow_Button : ButtonHandler
    {

        protected override void OnClick()
        {
            Instantiate(Prefabs.PreGameWindow, UI.Canvas);
        }

    }
}
