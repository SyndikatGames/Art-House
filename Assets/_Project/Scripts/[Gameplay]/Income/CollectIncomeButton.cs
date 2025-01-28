using VG2;
using Zenject;

public class CollectIncomeButton : ButtonHandler
{
    [Inject] private IncomeController _incomeController;


    protected override void OnClick()
    {
        float oldMoney = GameState.money.Value;
        _incomeController.CollectCurrentIncome();
        new EarnAnimation(transform.position, UI.MoneyValue, oldMoney, GameState.money.Value, EarnAnimationType.Money);
    }
}
