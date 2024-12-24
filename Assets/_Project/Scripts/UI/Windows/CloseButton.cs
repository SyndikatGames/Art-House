using UnityEngine;
using VG2;


public class CloseButton : ButtonHandler
{
    [SerializeField] private GameObject _window;

    protected override void OnClick() => Destroy(_window);

    
}