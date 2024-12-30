using UnityEngine;
using VG2;
using Zenject;

public class RunUnboxingButton : ButtonHandler
{
    private enum ButtonType { Single, Group }

    [SerializeField] private BoxWindowView _boxWindow;
    [SerializeField] private ButtonType _buttonType;
    
    [Inject] private UnboxingController _unboxingController;
    [Inject] private ShopController _shopController;


    protected override void OnClick()
    {
        int boxesAmount = BoxCalculator.GetBoxesAmount(_boxWindow.BoxType);

        if (boxesAmount > 0) // Unbox
        {
            int unboxableBoxesAmount = _buttonType == ButtonType.Single ? 1 : boxesAmount;
            _unboxingController.RunUnboxing(_boxWindow.BoxType, unboxableBoxesAmount);
            Destroy(_boxWindow.gameObject);
        }
        else // Try purchase and unbox
        {
            int purchasableBoxesAmount = _buttonType == ButtonType.Single ? 1 : ConfigHub.Shop.BoxesInGroup;

            if (_shopController.TryPurchaseBox(_boxWindow.BoxType, purchasableBoxesAmount))
            {
                _unboxingController.RunUnboxing(_boxWindow.BoxType, purchasableBoxesAmount);
                Destroy(_boxWindow.gameObject);
            }
        }

    }



}
