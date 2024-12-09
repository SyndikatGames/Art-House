using DG.Tweening;
using UnityEngine.SceneManagement;
using VG;

namespace PrizeClaw
{
    public class ExitGame_Button : ButtonHandler
    {

        protected override void OnClick()
        {
            DOTween.KillAll();
            SceneManager.LoadScene(Key_Scene.main_scene);
        }

    }
}
