using UnityEngine;
using VG2;
using Zenject;


public class OpenBoxWindowButton : ButtonHandler
{
    [SerializeField] private BoxProductView _boxProductView;
    [Inject] private ShopController _shopController;


    protected override void OnClick() 
        => _shopController.OpenBoxWindow(_boxProductView.BoxType);

    
}