using UnityEngine;
using R3;
using System.Threading.Tasks;


public class MergeItemsTutorialStep : TutorialStep
{
    private EventController _eventController;
    private GameObject _cursor;

    public const int MERGE_ITEMS_REQUIRE = 3;

    public MergeItemsTutorialStep(EventController eventController)
    {
        _eventController = eventController;
    }



    public override void Run()
    {
        Disposables.Add(_eventController.OnItemsMerged.Subscribe(_ => OnItemsMerged()));
        Disposables.Add(_eventController.OnCardTaken.Subscribe(_ => OnCardTaken()));
        Disposables.Add(_eventController.OnCardReleased.Subscribe(_ => OnCardReleased()));

        TaskController.SetTask(2);

        RunCursorAnimation();

    }

    private void RunCursorAnimation()
    {
        if (_cursor != null) Object.Destroy(_cursor);

        var card = Dependencies.CardInventory.GetAnyCardByList(OpenBoxesTutorialStep.BoxItems);
        Vector2 from = card.transform.position;
        Vector2 to = MainCamera.Camera.WorldToScreenPoint
            (ItemPlacing.PlacedItems.Find(item => item.ItemType == card.Data.itemType).transform.position);

        var cursorTween = Object.Instantiate(Dependencies.PressMoveCursorPrefab, UI.Canvas);
        cursorTween.Run(from, to);
        _cursor = cursorTween.gameObject;
    }


    private void OnCardReleased()
    {
        _cursor.SetActive(true);
    }

    private void OnCardTaken()
    {
        _cursor.SetActive(false);
    }

    private async void OnItemsMerged()
    {
        

        int mergedItems = 0;
        foreach (var item in ItemPlacing.PlacedItems)
            if (item.RarityType == RarityType.Rare) mergedItems++;

        if (mergedItems >= MERGE_ITEMS_REQUIRE)
            StepCompleted();

        else
        {
            await Task.Delay(100);
            RunCursorAnimation();
        }
    }



}
