using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            BoxOpeningModel boxOpeningModel = new BoxOpeningModel
            {
                boxRarityType = RarityType.Rare,
                generatedCards = new List<CardModel>
                {
                    new CardModel
                    {
                        itemType = ItemType.Apple,
                        rarityType = RarityType.Rare,
                        amount = 1,
                    },
                    new CardModel
                    {
                        itemType = ItemType.Door,
                        rarityType = RarityType.Rare,
                        amount = 1,
                    }

                }
            };

            Instantiate(Prefabs.BoxOpeningWindow, UI.Canvas)
                .Display(boxOpeningModel);



        }
    }
}
