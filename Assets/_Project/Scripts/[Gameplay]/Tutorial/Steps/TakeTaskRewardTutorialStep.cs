using R3;

public class TakeTaskRewardTutorialStep : TutorialStep
{
    private int _taskIndex;


    public TakeTaskRewardTutorialStep(int taskIndex)
    {
        _taskIndex = taskIndex;
    }



    public override void Run()
    {
        Disposables.Add(TaskController.OnTaskFinished.Subscribe(_ => OnTaskFinished()));

        TaskController.SetTask(_taskIndex);

    }

    private void OnTaskFinished()
    {
        StepCompleted();
    }
}
