using System;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemMoving : MonoBehaviour
{
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
    }

    private void OnBeginDrag(PointerEventData eventData)
    {
        ItemList.PlacedItems.Remove(_item);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 worldPointerPosition = Camera.main.ScreenToWorldPoint(eventData.position);

        if (TryPlace(_item, worldPointerPosition,
                out var resultPosition, out var resultGridPosition))
        {
            _item.transform.position = resultPosition;
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

        if (TryPlace(_item, worldPointerPosition, out _, out var resultGridPosition))
        {
            _item.SetPlace(resultGridPosition);
            print(resultGridPosition);
        }
            
    }


    private bool TryPlace(Item item, Vector2 worldPosition, 
        out Vector2 resultWorldPosition, out Vector3Int resultGridPosition)
    {
        var availablePlaceGrids = new List<PlaceGrid>();
        resultWorldPosition = default;
        resultGridPosition = default;

        foreach (var placedItem in ItemList.PlacedItems)
        {
            foreach (var placeGrid in placedItem.PlaceGrids)
                if (placeGrid.TryPlaceItem(item, worldPosition, out _, out _))
                    availablePlaceGrids.Add(placeGrid);
        }

        if (availablePlaceGrids.Count == 0) 
            return false;

        PlaceGrid selectedPlaceGrid = availablePlaceGrids[0];
        for (int i = 1; i < availablePlaceGrids.Count; i++)
        {
            var placeGrid = availablePlaceGrids[i];

            switch (selectedPlaceGrid.GridType)
            {
                case GridType.Floor:
                    if (selectedPlaceGrid.Position.z < placeGrid.Position.z)
                        selectedPlaceGrid = placeGrid;
                    break;

                case GridType.ToLeft:
                    if (selectedPlaceGrid.Position.x < placeGrid.Position.x)
                        selectedPlaceGrid = placeGrid;
                    break;

                case GridType.ToRight:
                    if (selectedPlaceGrid.Position.y < placeGrid.Position.y)
                        selectedPlaceGrid = placeGrid;
                    break;
            }
        }

        selectedPlaceGrid.TryPlaceItem(item, worldPosition, 
            out resultWorldPosition, out resultGridPosition);

        return true;
    }

}
