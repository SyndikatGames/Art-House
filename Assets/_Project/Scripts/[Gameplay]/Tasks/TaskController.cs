using R3;
using UnityEngine;
using VG2;
using Zenject;

public class TaskController : ITickable
{
    public Observable<int> OnTaskFinished => _onTaskFinished; private Subject<int> _onTaskFinished = new();
    public Observable<int> OnTaskChanged => _onTaskChanged; private Subject<int> _onTaskChanged = new();
    public Observable<int> OnTaskProgressChanged => _onTaskProgressChanged; private Subject<int> _onTaskProgressChanged = new();

    public bool TaskExists { get; private set; } = false;
    public int TaskId { get; private set; } = -1;
    public int ProgressPercentage { get; private set; }

    private int PLACED_ITEMS_REQUIRE = 10;
    private int OPENED_BOXES_REQUIRE = 5;
    


    public void SetTask(int taskId)
    {
        TaskExists = true;
        if (TaskId != taskId)
        {
            TaskId = taskId;
            _onTaskChanged.OnNext(taskId);
        }
    }

    public void SetTaskProgress(int progressPercentage)
    {
        if (ProgressPercentage != progressPercentage)
        {
            ProgressPercentage = Mathf.Clamp(progressPercentage, 0, 100);
            _onTaskProgressChanged.OnNext(progressPercentage);
        }
    }


    public void FinishTask()
    {
        var taskData = ConfigHub.Tasks.GetTaskData(TaskId);

        switch (taskData.rewardType)
        {
            case TaskRewardType.CommonBox:
                BoxCalculator.AddBoxes(BoxType.Common, (int)taskData.rewardAmount);
                break;

            case TaskRewardType.RareBox:
                BoxCalculator.AddBoxes(BoxType.Rare, (int)taskData.rewardAmount);
                break;

            case TaskRewardType.EpicBox:
                BoxCalculator.AddBoxes(BoxType.Epic, (int)taskData.rewardAmount);
                break;

            case TaskRewardType.Money:
                GameState.money.Value += taskData.rewardAmount;
                break;
        }

        TaskExists = false;
        _onTaskFinished.OnNext(TaskId);
    }

    public void Tick()
    {
        switch (TaskId)
        {
            case 0: SetTaskProgress(PlaceItemsProgress); break;
            case 1: SetTaskProgress(OpenBoxesProgress); break;
            case 2: SetTaskProgress(MergeItemsProgress); break;
            case 3: SetTaskProgress(PlayPrizeClawProgress); break;
        }
    }

    private int PlaceItemsProgress
        => (int)((float)(ItemPlacing.PlacedItems.Count - 1) / PLACED_ITEMS_REQUIRE * 100);

    private int OpenBoxesProgress 
        => (int)(1f - (float)BoxCalculator.GetBoxesAmount(BoxType.Common) / OPENED_BOXES_REQUIRE) * 100;

    private int MergeItemsProgress
    {
        get
        {
            int mergedItems = 0;
            foreach (var item in ItemPlacing.PlacedItems)
                if (item.RarityType == RarityType.Rare) mergedItems++;

            return (int)((float)mergedItems / MergeItemsTutorialStep.MERGE_ITEMS_REQUIRE * 100) ;
        }
    }

    private int PlayPrizeClawProgress => 
        GameState.tutorialStepIndex.Value > TutorialController.PRIZE_CLAW_TUTORIAL_STEP_INDEX ? 100 : 0;


}
