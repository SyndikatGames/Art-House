using VG;

namespace PrizeClaw
{
    public class RunGame_Button : ButtonHandler
    {

        protected override void OnClick()
        {
            bool gameWasStarted = Saves.String[Key_Save.prize_claw_data].Value != string.Empty;

            if (!gameWasStarted)
                Saves.Float[Key_Save.prize_claw_tickets].Value -= 1f;

            SceneLoader.LoadScene(Key_Scene.prize_claw);
        }

    }
}
