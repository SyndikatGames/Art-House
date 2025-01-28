using System.Collections.Generic;
using System.Threading.Tasks;
using R3;
using UnityEngine;
using VG2;

public class OpenBoxesTutorialStep : TutorialStep
{
    private ShopController _shopController;
    private UnboxingController _unboxingController;
    private GameObject _prompt;

    public static List<ItemType> BoxItems { get; private set; } = new List<ItemType>
    {
        // For merging
        ItemType.RectCoffeeTable,
        ItemType.BunkBed,
        ItemType.WallClock,
        
        // Other
        ItemType.ManShoes,
        ItemType.Book,
    };


    public OpenBoxesTutorialStep(ShopController shopController, UnboxingController unboxingController)
    {
        _shopController = shopController;
        _unboxingController = unboxingController;
    }


    public override void Run()
    {
        Disposables.Add(_shopController.OnShopOpened.Subscribe(shop => OnShopOpened(shop)));

        TaskController.SetTask(1);

        var prompt = Object.Instantiate(Dependencies.LeftArrowPromptPrefab, Dependencies.ShopRect);
        prompt.text = "Открой магазин!";
        _prompt = prompt.gameObject;

    }

    private async void OnShopOpened(ShopView shop)
    {
        Disposables.Add(_shopController.OnBoxWindowOpened.Subscribe(boxWindow => OnBoxWindowOpened(boxWindow)));

        Object.Destroy(_prompt);

        await Task.Delay(100);
        _prompt = Object.Instantiate(Dependencies.CicleScaleCursorPrefab, shop.CommonBoxButtonRect);
        _prompt.transform.SetParent(UI.Canvas);



    }

    private async void OnBoxWindowOpened(BoxWindowView boxWindow)
    {
        Disposables.Add(GameState.CurrentRoom.boxesAmount.onChanged.Subscribe(_ => OnBoxesAmountChanged()));

        await Task.Delay(100);
        Object.Destroy(_prompt);
        _prompt = Object.Instantiate(Dependencies.CicleScaleCursorPrefab, boxWindow.OpenButtonRect);

        var generatedCards = new List<CardModel>();
        foreach (var item in BoxItems)
        {
            generatedCards.Add(new CardModel
            {
                amount = 1,
                itemType = item,
                rarityType = RarityType.Common,
            });
        }

        _unboxingController.SetNextUnboxingModel(new UnboxingModel
        {
            boxType = BoxType.Common,
            generatedCards = generatedCards,
        });
    }

    private void OnBoxesAmountChanged()
    {
        if (BoxCalculator.GetBoxesAmount(BoxType.Common) == 0)
            StepCompleted();
    }
}
