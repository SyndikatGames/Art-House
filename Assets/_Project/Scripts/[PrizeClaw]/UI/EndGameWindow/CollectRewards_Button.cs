using UnityEngine.SceneManagement;
using VG;

namespace PrizeClaw
{
    public class CollectRewards_Button : ButtonHandler
    {

        protected override void OnClick()
        {
            foreach (var reward in GameState.Current.Rewards)
                PrizeReleaser.Release(reward.Key, reward.Value);

            GameState.Current.Clear();
            SceneManager.LoadScene(Key_Scene.main_scene);
        }

    }
}
