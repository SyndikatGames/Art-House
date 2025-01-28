using R3;
using UnityEngine;

public class ReachNewLevelTutorialStep : TutorialStep
{
    private EventController _eventController;
    private GameObject _prompt;


    public ReachNewLevelTutorialStep(EventController eventController)
    {
        _eventController = eventController;
    }


    public override void RestoreContext()
    {
        base.RestoreContext();
        if (_prompt != null) Object.Destroy(_prompt);
    }


    public override void Run()
    {
        // TODO: Add return in game

        Disposables.Add(_eventController.OnNewLevelReached.Subscribe(_ => OnNewLevelReached()));
    }

    private void OnNewLevelReached()
    {
        TaskController.SetTask(0);
        StepCompleted();
    }


}
