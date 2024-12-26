using UnityEngine;
using VG2;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject] private UnboxingController _unboxingController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameState.money.Value++;
            CardCalculator.AddCard(new CardModel
            {
                amount = 2,
                itemType = ItemType.AlarmClock,
                rarityType = RarityType.Rare,
            });
        }
    }
}
