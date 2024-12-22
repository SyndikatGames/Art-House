using UnityEngine;
using VG;

public class AdWarning_Info : ReactiveView
{
    [SerializeField] private GameObject _warning;

    protected override void Subscribe()
    {
        Saves.Bool[Key_Save.ads_enabled].onChanged += Display;
    }

    protected override void Dispose()
    {
        Saves.Bool[Key_Save.ads_enabled].onChanged -= Display;
    }

    protected override void Display()
    {
        bool showWarning = Saves.Bool[Key_Save.ads_enabled].Value && !Hack.release;
        _warning.SetActive(showWarning);
    }
}
