using System;
using UnityEngine;
using VG2;

public class Events : MonoBehaviour
{
    public static event Action onNewLevelReached;
    public static event Action onItemPlaced; public static void ItemPlaced() => onItemPlaced?.Invoke();
    public static event Action onNewItemPlaced; public static void NewItemPlaced() => onNewItemPlaced?.Invoke();
    public static event Action onNewItemDestroyed; public static void NewItemDestroyed() => onNewItemDestroyed?.Invoke();
    public static event Action onItemsMerged; public static void ItemsMerged() => onItemsMerged?.Invoke();
    public static event Action onItemRotated; public static void ItemRotated() => onItemRotated?.Invoke();
    public static event Action onBoxesCollected; public static void BoxesCollected() => onBoxesCollected?.Invoke();




    public delegate void OnBoxOpened(Item item);
    public static event OnBoxOpened onBoxOpened; 
    public static void BoxOpened(Item item) => onBoxOpened?.Invoke(item);

    private int _previousRoomLevel;



    private void Awake()
    {
        /*
        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;

        Saves.String[Key_Save.room_data(roomIndex)].onChanged += OnRoomDataChanged;
        _previousRoomLevel = Configs.GetRoom(roomIndex).CurrentLevel;
        */
    }

    private void OnRoomDataChanged()
    {
        /*
        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;
        int currentLevel = Configs.GetRoom(roomIndex).CurrentLevel;
        if (_previousRoomLevel < currentLevel)
        {
            onNewLevelReached?.Invoke();
            _previousRoomLevel = currentLevel;
        }
            */


    }
}
