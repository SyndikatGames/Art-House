using UnityEngine;
using VG2;

public class ShowOfflineWindow_Event : MonoBehaviour
{
    private const float minOfflineTime = 15f;


    private void Start()
    {
        int roomIndex = GameState.currentRoomIndex.Value;

        /*
        bool windowAvailable = 
            Saves.Float[Key_Save.offline_time_seconds(roomIndex)].Value > minOfflineTime
            && Saves.Bool[Key_Save.tutorial_completed].Value;


        if (windowAvailable)
            Instantiate(Prefabs.OfflineWindow, UI.Canvas);
        */
    }


    

}
