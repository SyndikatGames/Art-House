using UnityEngine;
using VG2;
using Zenject;
using R3;

public class ShowReviewEvent : ReactiveEvent
{
    [SerializeField] private int _level;
    [Inject] private EventController _eventController;


    protected override void Subscribe()
    {
        disposables.Add(_eventController.OnNewLevelReached.Subscribe(level => OnNewLevelReached(level)));
    }

    private void OnNewLevelReached(int level)
    {
        if (level >= _level) Review.Request();
    }
}
