using VG2;


public class OpenStylesWindow_Button : ButtonHandler
{
    
    protected override void OnClick()
    {
        Instantiate(Prefabs.StylesWindow, UI.Canvas);
    }
    
}