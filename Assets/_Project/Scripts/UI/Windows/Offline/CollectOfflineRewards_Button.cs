using UnityEngine;
using VG2;


public class CollectOfflineRewards_Button : ButtonHandler
{
    [SerializeField] private GameObject _window;


    protected override void OnClick()
    {
        ReleaseOfflineTime();
        Destroy(_window);
    }

    private void ReleaseOfflineTime()
    {
        /*
        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;
        float offlineSeconds = Saves.Float[Key_Save.offline_time_seconds(roomIndex)].Value;

        float offlineBoxes = TotalRules.GetBoxesPerHour(roomIndex) / 3600f * offlineSeconds;
        Saves.Float[Key_Save.random_boxes(roomIndex)].Value += offlineBoxes;

        float offlineSoftMoney = TotalRules.HourGemIncome / 3600f * offlineSeconds;
        Saves.Float[Key_Save.money].Value += offlineSoftMoney;

        Saves.Float[Key_Save.offline_time_seconds(roomIndex)].Value = 0f;
        */
    }

}