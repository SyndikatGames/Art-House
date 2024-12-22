using VG;
using UnityEngine;
using TMPro;

public class PrestigeLevel_Info : ReactiveView
{
    [SerializeField] private TextMeshProUGUI _text;


    private const int roomIndex = 0;


    protected override void Subscribe()
    {
        Saves.String[Key_Save.room_data(roomIndex)].onChanged += Display;
    }
    
    protected override void Dispose()
    {
        Saves.String[Key_Save.room_data(roomIndex)].onChanged -= Display;
    }
    
    protected override void Display()
    {
        _text.text = Configs.GetRoom(roomIndex).CurrentLevel.ToString();
    }
    
}