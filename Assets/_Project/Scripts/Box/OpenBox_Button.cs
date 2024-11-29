using UnityEngine;
using VG;


public class OpenBox_Button : ButtonHandler
{
    [SerializeField] private Box _box;

    
    protected override void OnClick()
    {
        if (Box.OpeningAvailable && !Box.OpeningBlocked) _box.Open();
    }
    
}