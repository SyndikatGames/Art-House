using PrizeClaw;
using UnityEngine;
using R3;

public class PlayPrizeClawTutorialStep : TutorialStep
{
    private GameObject _prompt;
    private GameController _prizeClawController;


    public PlayPrizeClawTutorialStep(GameController prizeClawController)
    {
        _prizeClawController = prizeClawController;
    }


    public override void Run()
    {
        Disposables.Add(_prizeClawController.OnLaunchGameWindowOpened.Subscribe(window => OnLaunchGameWindowOpened(window)));

        TaskController.SetTask(3);

        var prompt = Object.Instantiate(Dependencies.LeftArrowPromptPrefab, Dependencies.PrizeClawButtonRect);
        prompt.text = "Сыграй в хватайку!";
        _prompt = prompt.gameObject;
    }

    private void OnLaunchGameWindowOpened(LaunchGameWindowView window)
    {
        Disposables.Add(_prizeClawController.OnGameLaunched.Subscribe(_ => OnGameLaunched()));

        Object.Destroy(_prompt);
        _prompt = Object.Instantiate(Dependencies.CicleScaleCursorPrefab, window.PlayButtonRect);
    }

    private void OnGameLaunched()
    {
        Object.Destroy(_prompt);
    }
}
