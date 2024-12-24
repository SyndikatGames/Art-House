using TMPro;
using UnityEngine;
using VG2;

public class MergeItems_TutorialStep : TutorialStep
{
    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private CursorMove_Tween _cursorTween;

    private ItemInteraction _itemDragging;


    public override void RestoreContext()
    {
        _promptText.gameObject.SetActive(false);
        _cursorTween.gameObject.SetActive(false);
        Events.onBoxOpened -= OnBoxOpened;

        if (_itemDragging != null)
            _itemDragging.onBeginDragging -= OnBeginDragging;

        Events.onItemsMerged -= OnItemsMerged;
    }

    public override void Run()
    {
        _promptText.gameObject.SetActive(true);
        _promptText.text = Localization.GetString("tutorial_open_all_boxes");
        Events.onBoxOpened += OnBoxOpened;
    }

    private void OnBoxOpened(Item item)
    {
        Events.onBoxOpened -= OnBoxOpened;

        var secondItem = FindSameItem(item);
        _promptText.text = Localization.GetString("tutorial_merge_same_items");

        _cursorTween.gameObject.SetActive(true);
        _cursorTween.Run(item.transform.position, secondItem.transform.position);

        _itemDragging = item.GetComponent<ItemInteraction>();
        _itemDragging.onBeginDragging += OnBeginDragging;

        Events.onItemsMerged += OnItemsMerged;
    }

    private void OnBeginDragging()
    {
        _itemDragging.onBeginDragging -= OnBeginDragging;
        _cursorTween.gameObject.SetActive(false);
    }

    private Item FindSameItem(Item item)
    {
        foreach (var placedItem in ItemList.PlacedItems)
            if (placedItem.ItemType == item.ItemType) return placedItem;

        throw new System.Exception($"Wrong item: {item.ItemType}");
    }

    private void OnItemsMerged() => StepCompleted();

    


}
