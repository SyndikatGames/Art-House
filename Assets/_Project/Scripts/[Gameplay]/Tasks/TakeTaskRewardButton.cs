using VG2;
using Zenject;

public class TakeTaskRewardButton : ButtonHandler
{

    [Inject] private TaskController _taskController;

    protected override void OnClick()
    {
        _taskController.FinishTask();
    }
}
