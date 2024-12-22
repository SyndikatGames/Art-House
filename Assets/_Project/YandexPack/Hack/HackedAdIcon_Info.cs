using VG;

public class HackedAdIcon_Info : ReactiveView
{
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
        bool showIcon = !Hack.release && Saves.Bool[Key_Save.ads_enabled].Value;
        gameObject.SetActive(showIcon);
    }


}
