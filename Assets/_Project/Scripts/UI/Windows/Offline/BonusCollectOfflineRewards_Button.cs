using UnityEngine;
using VG2;


public class BonusCollectOfflineRewards_Button : ButtonHandler
{
    [SerializeField] private OfflineWindow _offlineWindow;
    
    protected override void OnClick()
    {
        Ads.Rewarded.Show(Key_Ad.offline_bonus, onShown: (result) =>
        {
            if (result == Ads.Rewarded.Result.Success)
            {
                Destroy(_offlineWindow.gameObject);
                ReleaseOfflineTime();
            }

        });

    }


    private void ReleaseOfflineTime()
    {
        /*
        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;
        float offlineSeconds = Saves.Float[Key_Save.offline_time_seconds(roomIndex)].Value;

        float offlineBoxes = TotalRules.GetBoxesPerHour(roomIndex) / 3600f * offlineSeconds;
        Saves.Float[Key_Save.random_boxes(roomIndex)].Value += offlineBoxes * 2;

        float offlineSoftMoney = TotalRules.HourGemIncome / 3600f * offlineSeconds;
        Saves.Float[Key_Save.money].Value += offlineSoftMoney * 2;

        Saves.Float[Key_Save.offline_time_seconds(roomIndex)].Value = 0f;
        */
    }


}