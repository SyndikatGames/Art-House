using UnityEngine;
using VG2;


public class AcceptBoxOffer_Button : ButtonHandler
{
    [SerializeField] private GameObject _window;

    
    protected override void OnClick()
    {
        Ads.Rewarded.Show(Key_Ad.box_offer, onShown: (result) =>
        {
            if (result == Ads.Rewarded.Result.Success)
            {
                Destroy(_window);
            }

        });



    }
    
}