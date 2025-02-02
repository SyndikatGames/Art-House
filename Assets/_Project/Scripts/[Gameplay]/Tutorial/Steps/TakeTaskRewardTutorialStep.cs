using R3;
using VG2;

public class TakeTaskRewardTutorialStep : TutorialStep
{
    private int _taskIndex;


    public TakeTaskRewardTutorialStep(int taskIndex)
    {
        _taskIndex = taskIndex;
    }

    public override void RestoreContext()
    {
        base.RestoreContext();
        RaycastBlock.Disable();
    }


    public override void Run()
    {
        Disposables.Add(TaskController.OnTaskFinished.Subscribe(_ => OnTaskFinished()));

        TaskController.SetTask(_taskIndex);
        RaycastBlock.Concentrate(Dependencies.TakeTaskRewardImage);
    }

    private void OnTaskFinished()
    {
        StepCompleted();
    }
}
