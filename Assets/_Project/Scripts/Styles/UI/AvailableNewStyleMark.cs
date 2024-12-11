using UnityEngine;
using VG;

public class AvailableNewStyleMark : Info
{
    [SerializeField] private GameObject _mark;


    protected override void Subscribe()
    {
        Events.onNewLevelReached += UpdateValue;
        Saves.String[Key_Save.styles_is_new_data(0)].onChanged += UpdateValue;
    }

    protected override void Unsubscribe()
    {
        Events.onNewLevelReached -= UpdateValue;
        Saves.String[Key_Save.styles_is_new_data(0)].onChanged -= UpdateValue;
    }

    protected override void UpdateValue()
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
