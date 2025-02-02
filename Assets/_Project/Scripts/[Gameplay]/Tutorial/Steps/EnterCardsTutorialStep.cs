using System.Collections.Generic;
using R3;
using UnityEngine;
using VG2;

public class EnterCardsTutorialStep : TutorialStep
{
    private EventController _eventController;
    private GameObject _cursor;
    private GameObject _placePrompt;
    private List<PlaceGridData> _originRoomPlaceGrid;

    private bool _highlightPlace;


    private readonly List<ItemType> _startItems = new List<ItemType>
    {
        ItemType.Window,
        ItemType.AlarmClock,
        ItemType.Cup,
        ItemType.Chair,
        ItemType.BunkBed,

        ItemType.CarpetFluffy,
        ItemType.Kettle,
        ItemType.RectCoffeeTable,
        ItemType.SquareClothes,
        ItemType.WallClock,
    };

    public EnterCardsTutorialStep(EventController eventController, bool highlightPlace)
    {
        _eventController = eventController;
        _highlightPlace = highlightPlace;
    }

    private void OnItemPlaced()
    {
        StepCompleted();
    }

    private void OnCardReleased()
    {
        _cursor.SetActive(true);
    }

    private void OnCardTaken()
    {
        _cursor.SetActive(false);
    }


    public override void RestoreContext()
    {
        base.RestoreContext();
        if (_cursor != null) Object.Destroy(_cursor);
        if (_placePrompt != null) Object.Destroy(_placePrompt);
        Dependencies.Room.SetPlaceGridData(_originRoomPlaceGrid);
    }

    


    public override void Run()
    {
        Disposables.Add(_eventController.OnCardTaken.Subscribe(_ => OnCardTaken()));
        Disposables.Add(_eventController.OnCardReleased.Subscribe(_ => OnCardReleased()));
        Disposables.Add(_eventController.OnItemPlaced.Subscribe(_ => OnItemPlaced()));

        TaskController.SetTask(0);

        if (GameState.CurrentRoom.cards.Count == 0)
        {
            foreach (var item in _startItems)
                CardCalculator.AddCard(new CardModel { itemType = item, rarityType = RarityType.Common, amount = 1 });
        }
        

        var cursorTween = Object.Instantiate(Dependencies.PressAndFadeMoveCursorPrefab, UI.Canvas);

        Vector2 from = (Vector2)Dependencies.InventoryRect.position + Vector2.up * 50f;
        Vector2 to = ScreenCalculator.GetScreenCenter();

        cursorTween.Run(from, to);

        _cursor = cursorTween.gameObject;

        _originRoomPlaceGrid = Dependencies.Room.PlaceGridDataList;
        

        if (_highlightPlace)
        {
            Dependencies.Room.SetPlaceGridData(Dependencies.TutorialRoomPlaceGridDataList);
            _placePrompt = Object.Instantiate(Dependencies.RoomPlacePromptPrefab, null);
            _placePrompt.transform.position = Dependencies.RoomPlacePromptPrefab.transform.position;
        }

    }



}
