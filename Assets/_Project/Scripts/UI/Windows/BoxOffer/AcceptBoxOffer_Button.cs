using UnityEngine;
using VG;


public class AcceptBoxOffer_Button : ButtonHandler
{
    [SerializeField] private GameObject _window;

    
    protected override void OnClick()
    {
        Ads.Rewarded.Show(Key_Ad.box_offer, onShown: (result) =>
        {
            if (result == Ads.Rewarded.Result.Success)
            {
                int roomIndex = Saves.Int[Key_Save.current_room_index].Value;
                Saves.Float[Key_Save.random_boxes(roomIndex)].Value += 5f;
                Destroy(_window);
            }

        });



    }
    
}