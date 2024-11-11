using VG;

public class PrestigeLevel_Info : Info
{
    private const int roomIndex = 0;


    protected override void Subscribe()
    {
        Saves.String[Key_Save.room_data(roomIndex)].onChanged += UpdateValue;
    }
    
    protected override void Unsubscribe()
    {
        Saves.String[Key_Save.room_data(roomIndex)].onChanged -= UpdateValue;
    }
    
    protected override void UpdateValue()
    {
        text.text = Configs.GetRoom(roomIndex).GetCurrentLevel().ToString();
    }
    
}