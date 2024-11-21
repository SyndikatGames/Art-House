using UnityEngine;
using VG;


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

                int roomIndex = Saves.Int[Key_Save.current_room_index].Value;

                Saves.Float[Key_Save.random_boxes(roomIndex)].Value 
                    += _offlineWindow.BoxesAccumulated;

                Saves.Float[Key_Save.soft_money].Value 
                    += _offlineWindow.SoftMoneyAccumulated;


            }

        });



    }
    
}