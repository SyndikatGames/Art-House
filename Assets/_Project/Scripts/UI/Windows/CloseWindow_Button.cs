using UnityEngine;
using VG;


public class CloseWindow_Button : ButtonHandler
{
    [SerializeField] private GameObject _window;

    protected override void OnClick() => Destroy(_window);

    
}