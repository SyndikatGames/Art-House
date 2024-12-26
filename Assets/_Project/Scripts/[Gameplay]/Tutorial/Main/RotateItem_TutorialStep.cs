using UnityEngine;

public class RotateItem_TutorialStep : TutorialStep
{
    [SerializeField] private GameObject _prompt;



    public override void RestoreContext()
    {
        _prompt.SetActive(false);
        Events.onItemRotated -= OnItemRotated;
    }

    public override void Run()
    {
        var placedItem = ItemPlacing.PlacedItems.Find((item) => item.ItemType != ItemType.Room);

        _prompt.transform.position = placedItem.transform.position;
        _prompt.SetActive(true);

        Events.onItemRotated += OnItemRotated;
    }

    private void OnItemRotated() => StepCompleted();
}
