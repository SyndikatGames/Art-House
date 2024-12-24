using UnityEngine;
using VG2;

public class ShowReview_Event : MonoBehaviour
{
    [SerializeField] private int _level;

    private void OnEnable()
    {
        Events.onNewLevelReached += OnNewLevelReached;
    }

    private void OnDisable()
    {
        Events.onNewLevelReached -= OnNewLevelReached;
    }

    private void OnNewLevelReached()
    {
        /*
        if (Configs.GetRoom(0).CurrentLevel >= _level)
            Review.Request();
        */
    }

    



}
