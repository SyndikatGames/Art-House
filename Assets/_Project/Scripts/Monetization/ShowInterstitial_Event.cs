using UnityEngine;
using VG;

public class ShowInterstitial_Event : MonoBehaviour
{


    private void OnEnable()
    {
        Events.onItemPlaced += OnItemPlaced;
    }

    private void OnDisable()
    {
        Events.onItemPlaced -= OnItemPlaced;
    }


    private void OnItemPlaced()
    {
        Ads.Interstitial.Show(Key_Ad.interstitial);
    }


}
