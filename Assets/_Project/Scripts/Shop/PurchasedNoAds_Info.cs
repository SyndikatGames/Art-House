using UnityEngine;
using VG;

public class PurchasedNoAds_Info : Info
{
    [SerializeField] private GameObject _noPurchased;
    [SerializeField] private GameObject _purchased;


    protected override void Subscribe()
    {
        Saves.Bool[Key_Save.ads_enabled].onChanged += UpdateValue;
    }
    
    protected override void Unsubscribe()
    {
        Saves.Bool[Key_Save.ads_enabled].onChanged -= UpdateValue;
    }
    
    protected override void UpdateValue()
    {
        bool adsEnabled = Saves.Bool[Key_Save.ads_enabled].Value;

        _noPurchased.SetActive(adsEnabled);
        _purchased.SetActive(!adsEnabled);


    }




    
}