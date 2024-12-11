using System.Collections.Generic;
using VG;

public static class TutorialBoxOpening
{
    public const int boxesAmount = 9;

    private static List<ItemType> _items = new List<ItemType>()
    {
        ItemType.RectCoffeeTable,
        ItemType.Chair, // Not used
        ItemType.Chair,
        ItemType.CarpetFluffy,
        ItemType.Window,

        ItemType.Kettle,
        ItemType.Cup,
        ItemType.BunkBed,
        ItemType.WallClock,
        ItemType.RectCoffeeTable,
    };


    public const int startStep = 0;
    public const int endStep = 10;


    public static bool TutorialNow
    {
        get
        {
            int tutorialStep = Saves.Int[Key_Save.tutorial_step].Value;
            return startStep <= tutorialStep && tutorialStep <= endStep;
        }
    }

    public static Item GetTutorialItemPrefab()
    {
        int currentStep = Saves.Int[Key_Save.tutorial_step].Value;
        int itemIndex = currentStep - startStep;

        return Prefabs.GetItem(_items[itemIndex]);
    }



}
