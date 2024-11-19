using UnityEngine;

public class ShowNewRoomLevelWindow_Event : MonoBehaviour
{

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
        Instantiate(Prefabs.RoomWindow, UI.Canvas).OpenNewLevel();
    }


}
