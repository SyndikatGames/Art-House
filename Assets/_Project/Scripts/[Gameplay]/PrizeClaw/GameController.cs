using DG.Tweening;
using R3;
using UnityEngine.SceneManagement;
using VG2;

namespace PrizeClaw
{
    public class GameController
    {

        public Observable<Unit> OnGameLaunched => _onGameLaunched;
        private Subject<Unit> _onGameLaunched = new();

        public Observable<LaunchGameWindowView> OnLaunchGameWindowOpened => _onLaunchGameWindowOpened;
        private Subject<LaunchGameWindowView> _onLaunchGameWindowOpened = new();


        public void OpenLaunchGameWindow()
        {
            var window = SceneContainer.InstantiatePrefabFromComponent(ConfigHub.PrizeClaw.LaunchGameWindow, UI.Canvas);
            _onLaunchGameWindowOpened.OnNext(window);
        }


        public void RunGame()
        {
            if (GameState.prizeClaw.gameStarted)
                SceneLoader.LoadScene(Key_Scene.prize_claw);

            else
            {
                if (GameState.tickets.Value >= 1f)
                {
                    GameState.tickets.Value -= 1f;
                    SceneLoader.LoadScene(Key_Scene.prize_claw);
                }
            }

            _onGameLaunched.OnNext(Unit.Default);
        }


        public void FinishGame()
        {
            ReleaseRewards();
            GameState.prizeClaw.gameStarted = false;

            if (GameState.tutorialStepIndex.Value == TutorialController.PRIZE_CLAW_TUTORIAL_STEP_INDEX)
                GameState.tutorialStepIndex.Value++;

            DOTween.KillAll();
            RewardEarnAnimation.Available = true;
            SceneManager.LoadScene(Key_Scene.main_scene);
        }



        private void ReleaseRewards()
        {
            foreach (var reward in GameState.prizeClaw.rewards)
            {
                switch (reward.Key)
                {
                    case PrizeType.Money:
                        GameState.money.Value += reward.Value * ConfigHub.PrizeClaw.MoneyInsideReward;
                        break;

                    case PrizeType.CommonBox:
                        BoxCalculator.AddBoxes(BoxType.Common, reward.Value);
                        break;

                    case PrizeType.RareBox:
                        BoxCalculator.AddBoxes(BoxType.Rare, reward.Value);
                        break;

                    case PrizeType.EpicBox:
                        BoxCalculator.AddBoxes(BoxType.Epic, reward.Value);
                        break;
                }
            }

        }


    }
}



