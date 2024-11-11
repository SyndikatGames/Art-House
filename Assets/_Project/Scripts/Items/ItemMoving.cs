using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemMoving : MonoBehaviour
{
    public static Item DraggableItem { get; private set; } = null;


    [SerializeField] private Item _item;
    [SerializeField] private InteractionHandler _interactionHandler;


    private void Awake()
    {
        _interactionHandler.onBeginDrag += OnBeginDrag;
        _interactionHandler.onDrag += OnDrag;
        _interactionHandler.onEndDrag += OnEndDrag;
        _interactionHandler.onClick += OnClick;
    }



    private void OnClick(PointerEventData eventData)
    {
        if (_item.PlaceType != ItemPlaceType.Floor) return;

        if (_item.CurrentSide == Side.Left) 
            _item.SetSide(Side.Right);

        else if (_item.CurrentSide == Side.Right) 
            _item.SetSide(Side.Left);

        ItemList.UpdateItems();
    }

    private void OnBeginDrag(PointerEventData eventData)
    {
        if (_item.ChildItems.Count != 0)
        {
            foreach (var item in _item.ChildItems)
                item.Highlight();

            _interactionHandler.DisableDragAction();
            return;
        }

        _item.ParentItem?.ChildItems.Remove(_item);
        ItemList.PlacedItems.Remove(_item);
        DraggableItem = _item;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 worldPointerPosition = Camera.main.ScreenToWorldPoint(eventData.position);

        if (TryPlace(_item, worldPointerPosition, out _, out var resultGridPosition))
        {
            _item.SetTransparent(false);
            _item.Placing(resultGridPosition);
        }
        else
        {
            _item.transform.position = worldPointerPosition;
            _item.SetTransparent(true);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 worldPointerPosition
                = Camera.main.ScreenToWorldPoint(eventData.position);

        if (TryPlace(_item, worldPointerPosition, out Item parent, out var resultGridPosition))
        {
            _item.SetPlace(resultGridPosition, parent);
            ItemList.UpdateItems();
        }
            

        DraggableItem = null;
    }


    private bool TryPlace(Item item, Vector2 worldPosition, 
        out Item parent, out Vector3Int resultGridPosition)
    {
        var availablePlaceGrids = new List<PlaceGrid>();
        var availableParents = new List<Item>();

        resultGridPosition = default;
        parent = null;

        foreach (var placedItem in ItemList.PlacedItems)
        {
            foreach (var placeGrid in placedItem.PlaceGrids)
                if (placeGrid.TryPlaceItem(item, worldPosition, out _))
                {
                    availablePlaceGrids.Add(placeGrid);
                    availableParents.Add(placedItem);
                }
                    
        }

        if (availablePlaceGrids.Count == 0) 
            return false;

        int selectedIndex = 0;
        for (int i = 1; i < availablePlaceGrids.Count; i++)
        {
            PlaceGrid selectedPlaceGrid = availablePlaceGrids[selectedIndex];
            var placeGrid = availablePlaceGrids[i];

            switch (selectedPlaceGrid.GridType)
            {
                case GridType.Floor:
                    if (selectedPlaceGrid.Position.z < placeGrid.Position.z)
                        selectedIndex = i;
                    break;

                case GridType.ToLeft:
                    if (selectedPlaceGrid.Position.x < placeGrid.Position.x)
                        selectedIndex = i;
                    break;

                case GridType.ToRight:
                    if (selectedPlaceGrid.Position.y < placeGrid.Position.y)
                        selectedIndex = i;
                    break;
            }
        }

        availablePlaceGrids[selectedIndex]
            .TryPlaceItem(item, worldPosition, out resultGridPosition);

        parent = availableParents[selectedIndex];

        return true;
    }

}
