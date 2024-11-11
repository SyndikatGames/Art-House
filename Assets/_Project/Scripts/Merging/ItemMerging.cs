using UnityEngine;
using UnityEngine.EventSystems;

public class ItemMerging : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private InteractionHandler _interactionHandler;

    private void Awake()
    {
        _interactionHandler.onBeginDrag += OnBeginDrag;
        _interactionHandler.onEndDrag += OnEndDrag;
        _interactionHandler.onPointerEnter += OnPointerEnter;
        _interactionHandler.onPointerExit += OnPointerExit;
        _interactionHandler.onDrop += OnDrop;
    }

    private void OnDrop(PointerEventData data)
    {
        if (ItemMoving.DraggableItem != null && MergingAvailable(ItemMoving.DraggableItem, _item))
        {
            ItemList.PlacedItems.Remove(ItemMoving.DraggableItem);
            Destroy(ItemMoving.DraggableItem.gameObject);

            RarityType newRarity = (RarityType)((int)_item.RarityType + 1);
            _item.SetRarity(newRarity);
            _item.Highlighter.Stop(HighlightType.MergingSelect);

            ItemList.UpdateItems();
        }


    }

    private void OnPointerExit(PointerEventData data)
    {
        if (ItemMoving.DraggableItem == null) return;

        if (MergingAvailable(ItemMoving.DraggableItem, _item))
        {
            _item.Highlighter.Stop(HighlightType.MergingSelect);
            SetMergingHiglight(true);
        }


    }

    private void OnPointerEnter(PointerEventData data)
    {
        if (ItemMoving.DraggableItem == null) return;

        if (MergingAvailable(ItemMoving.DraggableItem, _item))
        {
            SetMergingHiglight(false);
            _item.Highlighter.Highlight(HighlightType.MergingSelect);
        }
    }

    private void OnEndDrag(PointerEventData data)
    {
        SetMergingHiglight(false);
    }

    private void OnBeginDrag(PointerEventData data)
    {
        if (_item.ChildItems.Count > 0) return;
        SetMergingHiglight(true);
    }




    private void SetMergingHiglight(bool value)
    {
        foreach (var item in ItemList.PlacedItems)
        {
            if (MergingAvailable(item, _item))
            {
                if (value) item.Highlighter.Highlight(HighlightType.MergingAvailable);
                else item.Highlighter.Stop(HighlightType.MergingAvailable);
            }
                
        }
    }


    private bool MergingAvailable(Item item1, Item item2)
        => item1.RarityType == item2.RarityType && item1.ItemType == item2.ItemType
        && item1.RarityType != RarityType.Legendary;

}
