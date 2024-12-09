using DG.Tweening;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    

    public static void LoadScene(string sceneKey)
    {
        Fader.Fade(onFaded: () =>
        {
            DOTween.KillAll();
            SceneManager.LoadScene(sceneKey);
        });
    }


}
