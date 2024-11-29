using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemMoveHandler
{
    public static Item DraggableItem { get; private set; } = null;

    private Item _item;
    private Vector2 _beforeWorldPosition;
    private Vector3Int _beforePosition;


    public ItemMoveHandler(Item item)
    {
        _item = item;
    }


    public bool OnClick()
    {
        if (_item.PlaceType != ItemPlaceType.Floor) return false;


        if (_item.RotateAvailable)
        {
            if (_item.CurrentSide == Side.Left)
                _item.SetSide(Side.Right);

            else if (_item.CurrentSide == Side.Right)
                _item.SetSide(Side.Left);

            ItemList.UpdateItems();
            Events.ItemRotated();
            return true;
        }

        return false;
    }

    public bool OnBeginDrag()
    {
        if (_item.ChildItems.Count != 0)
        {
            foreach (var item in _item.ChildItems)
                item.Highlighter.Highlight(HighlightType.Overlap);

            return false;
        }

        if (_item.ParentItem != null)
            _item.ParentItem?.ChildItems.Remove(_item);

        _item.BlockRaycast = false;
        ItemList.PlacedItems.Remove(_item);
        DraggableItem = _item;

        if (_item.ParentItem != null) _beforePosition = _item.Position;
        else _beforeWorldPosition = _item.transform.position;

        _beforeWorldPosition = _item.transform.position;

        return true;
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

        if (TryPlace(_item, worldPointerPosition, out var placeGrid, out var resultGridPosition))
        {
            _item.SetPlace(resultGridPosition, placeGrid);
            ItemList.UpdateItems();
        }
        else
        {
            if (_item.ParentItem != null)
                _item.SetPlace(_beforePosition, _item.ParentGrid);

            else _item.transform.position = _beforeWorldPosition;

            _item.SetTransparent(false);
        }
            

        DraggableItem = null;
        _item.BlockRaycast = true;
    }


    private bool TryPlace(Item item, Vector2 worldPosition, 
        out PlaceGrid placeGrid, out Vector3Int resultGridPosition)
    {
        var availablePlaceGrids = new List<PlaceGrid>();

        resultGridPosition = default;
        placeGrid = null;

        foreach (var placedItem in ItemList.PlacedItems)
        {
            foreach (var itemPlaceGrid in placedItem.PlaceGrids)
                if (itemPlaceGrid.TryPlaceItem(item, worldPosition, out _))
                    availablePlaceGrids.Add(itemPlaceGrid);
                    
        }

        if (availablePlaceGrids.Count == 0) 
            return false;

        int selectedIndex = 0;
        for (int i = 1; i < availablePlaceGrids.Count; i++)
        {
            PlaceGrid selectedPlaceGrid = availablePlaceGrids[selectedIndex];
            var itemPlaceGrid = availablePlaceGrids[i];

            switch (selectedPlaceGrid.GridType)
            {
                case GridType.Floor:
                    if (selectedPlaceGrid.Position.z < itemPlaceGrid.Position.z)
                        selectedIndex = i;
                    break;

                case GridType.ToLeft:
                    if (selectedPlaceGrid.Position.x < itemPlaceGrid.Position.x)
                        selectedIndex = i;
                    break;

                case GridType.ToRight:
                    if (selectedPlaceGrid.Position.y < itemPlaceGrid.Position.y)
                        selectedIndex = i;
                    break;
            }
        }

        availablePlaceGrids[selectedIndex]
            .TryPlaceItem(item, worldPosition, out resultGridPosition);

        placeGrid = availablePlaceGrids[selectedIndex];

        return true;
    }

}
