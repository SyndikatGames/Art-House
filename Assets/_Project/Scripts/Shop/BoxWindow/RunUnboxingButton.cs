using UnityEngine;
using VG2;
using Zenject;

public class RunUnboxingButton : ButtonHandler
{
    private enum ButtonType { Single, Group }

    [SerializeField] private BoxWindowView _boxWindowView;
    [SerializeField] private ButtonType _buttonType;
    
    [Inject] private UnboxingController _unboxingController;
    [Inject] private ShopController _shopController;


    protected override void OnClick()
    {
        /*
        var model = _boxWindowView.Model;

        bool unboxingMode = model.currentBoxAmount > 0;
        int boxesAmount = 1;
        if (_buttonType == ButtonType.Group)
        {
            if (unboxingMode) boxesAmount = model.currentBoxAmount;
            else boxesAmount = model.boxesInGroupAmount;
        } 

        if (unboxingMode) _unboxingController.RunUnboxing(model.boxType, boxesAmount);

        else if (_shopController.TryBuyBox(model.boxType, boxesAmount))
            _unboxingController.RunUnboxing(model.boxType, boxesAmount);
        */

    }
}
