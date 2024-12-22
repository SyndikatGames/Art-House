using VG;
using UnityEngine;
using TMPro;

public class MoneyView : ReactiveView
{
    [SerializeField] private TextMeshProUGUI _text;

    protected override void Subscribe()
    {
        Saves.Float[Key_Save.money].onChanged += Display;
    }
    
    protected override void Dispose()
    {
        Saves.Float[Key_Save.money].onChanged -= Display;
    }
    
    protected override void Display()
    {
        _text.text = Saves.Float[Key_Save.money].Value.ToShortNumber();
    }
    
}