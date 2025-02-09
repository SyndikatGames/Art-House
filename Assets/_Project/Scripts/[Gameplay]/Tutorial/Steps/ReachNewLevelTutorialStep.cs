using R3;
using UnityEngine;

public class ReachNewLevelTutorialStep : TutorialStep
{
    private EventController _eventController;
    private GameObject _prompt;
    private int _taskIndex;


    public ReachNewLevelTutorialStep(EventController eventController, int taskIndex)
    {
        _eventController = eventController;
        _taskIndex = taskIndex;
    }


    public override void RestoreContext()
    {
        base.RestoreContext();
        if (_prompt != null) Object.Destroy(_prompt);
    }


    public override void Run()
    {
        Disposables.Add(_eventController.OnNewLevelReached.Subscribe(_ => OnNewLevelReached()));
        TaskController.SetTask(_taskIndex);
    }

    private void OnNewLevelReached()
    {
        
        StepCompleted();
    }


}
