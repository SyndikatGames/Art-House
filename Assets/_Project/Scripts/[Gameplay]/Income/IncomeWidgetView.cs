using TMPro;
using UnityEngine;
using VG2;
using R3;
using Zenject;
using UnityEngine.UI;

public class IncomeWidgetView : ReactiveView
{
    [SerializeField] private TextMeshProUGUI _moneyAccumulatedText;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Button _collectButton;


    [Inject] private IncomeController _incomeController;


    protected override void Subscribe()
    {
        disposables.Add(GameState.CurrentRoom.accumulatedMoney.Subscribe(_ => Display()));
    }


    protected override void Display()
    {
        _moneyAccumulatedText.text = GameState.CurrentRoom.accumulatedMoney.Value.ToShortNumber();
        _collectButton.interactable = GameState.CurrentRoom.accumulatedMoney.Value >= 1f;
    }


    private void Update()
    {
        _fillImage.fillAmount = 1f - _incomeController.TimeToPutIncomeLeft / ConfigHub.Income.PutIncomeEverySeconds;
    }



}
