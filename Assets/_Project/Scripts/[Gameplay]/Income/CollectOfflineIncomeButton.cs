using UnityEngine;
using VG2;
using Zenject;

public class CollectOfflineIncomeButton : ButtonHandler
{
    private enum ButtonType { Standart, Bonus }

    [SerializeField] private ButtonType _buttonType;

    [Inject] private IncomeController _incomeController;


    private const float BONUS_MULTIPLIER = 2f;

    protected override void OnClick()
    {
        float multiplier = _buttonType == ButtonType.Bonus ? BONUS_MULTIPLIER : 1f;
        _incomeController.CollectOfflineIncome(multiplier);
    }

}
