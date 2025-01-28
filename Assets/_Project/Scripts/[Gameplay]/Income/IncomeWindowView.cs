using TMPro;
using UnityEngine;
using VG2;
using Zenject;

public class IncomeWindowView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _offlineIncomeText;

    private IncomeController _incomeController;


    [Inject]
    private void Construct(IncomeController incomeController)
    {
        _incomeController = incomeController;
        Display();
    }

    private void Display()
    {
        _offlineIncomeText.text = $"<sprite=0> {GameState.CurrentRoom.accumulatedMoney.Value.ToShortNumber()}";
    }


}
