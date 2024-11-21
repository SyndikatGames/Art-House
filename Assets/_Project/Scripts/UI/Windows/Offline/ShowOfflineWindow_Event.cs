using UnityEngine;
using VG;

public class ShowOfflineWindow_Event : MonoBehaviour
{
    private const float minOfflineTime = 15f;


    private void Start()
    {
        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;

        if (Saves.Float[Key_Save.offline_time_seconds(roomIndex)].Value > minOfflineTime)
            Instantiate(Prefabs.OfflineWindow, UI.Canvas);

        ReleaseOfflineTime();
    }


    private void ReleaseOfflineTime()
    {
        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;
        float offlineSeconds = Saves.Float[Key_Save.offline_time_seconds(roomIndex)].Value;

        float offlineBoxes = TotalRules.GetBoxesPerHour(roomIndex) / 3600f * offlineSeconds;
        Saves.Float[Key_Save.random_boxes(roomIndex)].Value += offlineBoxes;

        float offlineSoftMoney = TotalRules.HourGemIncome / 3600f * offlineSeconds;
        Saves.Float[Key_Save.soft_money].Value += offlineSoftMoney;

        Saves.Float[Key_Save.offline_time_seconds(roomIndex)].Value = 0f;
    }

}
