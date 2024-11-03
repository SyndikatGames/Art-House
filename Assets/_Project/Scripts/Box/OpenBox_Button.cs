using UnityEngine;
using VG;


public class OpenBox_Button : ButtonHandler
{
    [SerializeField] private Box _box;

    
    protected override void OnClick()
    {
        _box.Open();
    }
    
}