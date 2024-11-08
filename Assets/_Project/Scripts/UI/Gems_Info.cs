using VG;

public class Gems_Info : Info
{
    
    protected override void Subscribe()
    {
        Saves.Int[Key_Save.gems].onChanged += UpdateValue;
    }
    
    protected override void Unsubscribe()
    {
        Saves.Int[Key_Save.gems].onChanged -= UpdateValue;
    }
    
    protected override void UpdateValue()
    {
        text.text = Saves.Int[Key_Save.gems].Value.ToString();
    }
    
}