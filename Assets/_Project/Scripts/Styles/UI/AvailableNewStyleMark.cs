using UnityEngine;
using VG;

public class AvailableNewStyleMark : ReactiveView
{
    [SerializeField] private GameObject _mark;


    protected override void Subscribe()
    {
        Events.onNewLevelReached += Display;
        Saves.String[Key_Save.styles_is_new_data(0)].onChanged += Display;
    }

    protected override void Dispose()
    {
        Events.onNewLevelReached -= Display;
        Saves.String[Key_Save.styles_is_new_data(0)].onChanged -= Display;
    }

    protected override void Display()
    {
        int roomLevel = Configs.GetRoom(0).CurrentLevel;

        bool hasNewStyles = false;
        for (int i = 0; i <= roomLevel; i++)
            if (Saves.StyleIsNew(i))
            {
                hasNewStyles = true;
                break;
            }

        _mark.SetActive(hasNewStyles);
    }
}
