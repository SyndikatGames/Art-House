using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;


public class MergeItemsTutorialStep : TutorialStep
{
    private EventController _eventController;
    private GameObject _cursor;

    public const int MERGE_ITEMS_REQUIRE = 3;

    public MergeItemsTutorialStep(EventController eventController)
    {
        _eventController = eventController;
    }


    public override void RestoreContext()
    {
        base.RestoreContext();
        Dependencies.ShopButtonImage.raycastTarget = true;
    }


    public override void Run()
    {
        Disposables.Add(_eventController.OnItemsMerged.Subscribe(async _ => await OnItemsMerged()));
        Disposables.Add(_eventController.OnCardTaken.Subscribe(_ => OnCardTaken()));
        Disposables.Add(_eventController.OnCardReleased.Subscribe(_ => OnCardReleased()));

        TaskController.SetTask(2);

        RunCursorAnimation();

        Dependencies.ShopButtonImage.raycastTarget = false;
    }

    private void RunCursorAnimation()
    {
        if (_cursor != null) Object.Destroy(_cursor);

        var card = Dependencies.CardInventory.GetAnyCardByList(OpenBoxesTutorialStep.BoxItems);

        if (card != null)
        {
            Vector2 from = card.transform.position;
            Vector2 to = MainCamera.Camera.WorldToScreenPoint
                (ItemPlacing.PlacedItems.Find(item => item.ItemType == card.Data.itemType).transform.position);

            var cursorTween = Object.Instantiate(Dependencies.PressMoveCursorPrefab, UI.Canvas);
            cursorTween.Run(from, to, onCompleted: RunCursorAnimation);
            _cursor = cursorTween.gameObject;
        }
    }


    private void OnCardReleased()
    {
        if (_cursor != null) _cursor.SetActive(true);
    }

    private void OnCardTaken()
    {
        if (_cursor != null) _cursor.SetActive(false);
    }

    private async UniTask OnItemsMerged()
    {
        int mergedItems = 0;
        foreach (var item in ItemPlacing.PlacedItems)
            if (item.RarityType == RarityType.Rare) mergedItems++;

        if (mergedItems >= MERGE_ITEMS_REQUIRE)
            StepCompleted();

        else
        {
            await UniTask.Yield();
            RunCursorAnimation();
        }
    }



}
