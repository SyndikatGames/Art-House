using System.Collections.Generic;
using UnityEngine;
using VG;

public class Test : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            var size = Saves.RoomSize;
            Saves.AddCard(new CardData()
            {
                rarityType = RarityType.Rare,
                amount = 2,
                itemType = ItemType.AlarmClock,
            });



        }
    }
}
