using System;
using UnityEngine;
using VG;

public class Events : MonoBehaviour
{
    public static event Action onNewLevelReached;

    private int _previousRoomLevel;



    private void Awake()
    {
        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;

        Saves.String[Key_Save.room_data(roomIndex)].onChanged += OnRoomDataChanged;
        _previousRoomLevel = Configs.GetRoom(roomIndex).CurrentLevel;
    }

    private void OnRoomDataChanged()
    {
        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;
        int currentLevel = Configs.GetRoom(roomIndex).CurrentLevel;
        if (_previousRoomLevel < currentLevel)
        {
            onNewLevelReached?.Invoke();
            _previousRoomLevel = currentLevel;
        }
            


    }
}
