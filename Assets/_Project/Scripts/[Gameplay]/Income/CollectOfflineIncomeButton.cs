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
        if (_buttonType == ButtonType.Bonus)
        {
            Ads.Rewarded.Show(string.Empty, onShown: result =>
            {
                if (result == Ads.Rewarded.Result.Success)
                    _incomeController.CollectOfflineIncome(BONUS_MULTIPLIER);
            });
        }
        else _incomeController.CollectOfflineIncome(1f);
    }

}
