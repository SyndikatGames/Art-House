using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Configs/Tasks", fileName = "Tasks")]
public class TasksConfig : ScriptableObject
{
    [System.Serializable]
    private struct RewardSprite
    {
        public TaskRewardType rewardType;
        public Sprite sprite;
    }



    [SerializeField] private List<RewardSprite> _rewardSprites;
    [SerializeField] private List<TaskData> _tasks;


    
    public TaskData GetTaskData(int id) => _tasks.Find(task => task.id == id);

    public Sprite GetRewardIcon(TaskRewardType rewardType) 
        => _rewardSprites.Find(sprite => sprite.rewardType == rewardType).sprite;


}
