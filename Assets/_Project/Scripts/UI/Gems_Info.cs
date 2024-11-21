using VG;

public class Gems_Info : Info
{
    
    protected override void Subscribe()
    {
        Saves.Float[Key_Save.soft_money].onChanged += UpdateValue;
    }
    
    protected override void Unsubscribe()
    {
        Saves.Float[Key_Save.soft_money].onChanged -= UpdateValue;
    }
    
    protected override void UpdateValue()
    {
        text.text = Saves.Float[Key_Save.soft_money].Value.ToShortNumber();
    }
    
}