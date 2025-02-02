using UnityEngine;
using VG2;

[System.Serializable]
public struct TaskData
{
    public int id;
    public TaskRewardType rewardType;
    public float rewardAmount;

    [SerializeField] private string _descriptionLocalizationKey;

    public string Description => Localization.GetString(_descriptionLocalizationKey);

}
