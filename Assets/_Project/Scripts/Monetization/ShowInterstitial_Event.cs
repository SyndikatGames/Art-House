using R3;
using VG2;
using Zenject;

public class ShowInterstitial_Event : ReactiveEvent
{
    [Inject] private EventController _eventController;

    protected override void Subscribe()
    {
        disposables.Add(_eventController.OnItemPlaced.Subscribe(_ => OnItemPlaced()));

    }


    private void OnItemPlaced()
    {
        if (GameState.adsEnabled.Value && TutorialController.InterstitialAdsAvailable)
            Ads.Interstitial.Show(Key_Ad.interstitial);
    }

    
}
