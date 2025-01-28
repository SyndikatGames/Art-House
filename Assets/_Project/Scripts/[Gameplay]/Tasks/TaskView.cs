using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;
using Zenject;

public class TaskView : ReactiveView
{
    [SerializeField] private MoveRectTween _moveTween;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _rewardImage;
    [SerializeField] private Image _progressFill;
    [SerializeField] private TextMeshProUGUI _rewardAmountText;
    [SerializeField] private GameObject _completeMark;
    [SerializeField] private GameObject _shine;
    [SerializeField] private Button _takeRewardButton;

    [Inject] private TaskController _taskController;


    protected override void Subscribe()
    {
        disposables.Add(_taskController.OnTaskChanged.Subscribe(_ => Display()));
        disposables.Add(_taskController.OnTaskProgressChanged.Subscribe(_ => Display()));
        disposables.Add(_taskController.OnTaskFinished.Subscribe(taskId => OnTaskFinished(taskId)));
    }

    private void OnTaskFinished(int taskId)
    {
        var taskData = ConfigHub.Tasks.GetTaskData(taskId);

        if (taskData.rewardType == TaskRewardType.Money)
            new EarnAnimation(transform.position, UI.MoneyValue, GameState.money.PreviousValue, GameState.money.Value, EarnAnimationType.Money);

        else
        {
            var sprite = ConfigHub.Tasks.GetRewardIcon(taskData.rewardType);
            int imageAmount = (int)Mathf.Min(5, taskData.rewardAmount);
            new EarnAnimation(transform.position, UI.Shop, sprite, imageAmount);
        }
    }

    protected override void Display()
    {
        if (_taskController.TaskExists)
        {
            var taskData = ConfigHub.Tasks.GetTaskData(_taskController.TaskId);

            _moveTween.Enter();
            _descriptionText.text = taskData.Description;

            _progressFill.fillAmount = _taskController.ProgressPercentage / 100f;

            _rewardImage.sprite = ConfigHub.Tasks.GetRewardIcon(taskData.rewardType);
            _rewardAmountText.text = taskData.rewardAmount.ToShortNumber();


            bool taskCompleted = _taskController.ProgressPercentage == 100;

            _shine.SetActive(taskCompleted);
            _completeMark.SetActive(taskCompleted);
            _takeRewardButton.interactable = taskCompleted;

        }
        else _moveTween.Return();





    }


    
}
