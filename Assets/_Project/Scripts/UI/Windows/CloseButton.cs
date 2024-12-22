using UnityEngine;
using VG;


public class CloseButton : ButtonHandler
{
    [SerializeField] private GameObject _window;

    protected override void OnClick() => Destroy(_window);

    
}