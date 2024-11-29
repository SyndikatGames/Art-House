using UnityEngine;
using DG.Tweening;
using System;
using VG;

public class Box : MonoBehaviour
{

    public static bool OpeningBlocked { get; set; } = false;
    public static bool OpeningAvailable { get; private set; } = true;

    public event Action onOpened;

    [SerializeField] private RarityType _rarityType;

    private Vector2 _originPosition;
    private Item _itemInstance;

    private const float itemMoveY = 0.5f;
    private const float showDuration = 0.5f;

    
    

    public void Open()
    {
        OpeningAvailable = false;

        _originPosition = transform.position;

        Item itemPrefab = null;

        if (TutorialBoxOpening.TutorialNow)
            itemPrefab = TutorialBoxOpening.GetTutorialItemPrefab();

        else itemPrefab = GetRandomItemPrefab();

        _itemInstance = Instantiate(itemPrefab, _originPosition, Quaternion.identity);
        _itemInstance.SetRarity(_rarityType);

        _itemInstance.SortingOrder = 10000;

        _itemInstance.transform.DOMoveY(_originPosition.y + itemMoveY, showDuration);

        _itemInstance.transform.localScale = Vector3.zero;
        _itemInstance.transform.DOScale(1f, showDuration);

        transform.DOMoveY(_originPosition.y - itemMoveY, showDuration);
        transform.DOScale(0f, showDuration);


        _itemInstance.onPlaced += OnItemPlaced;
        _itemInstance.onDestroyed += OnItemDestroyed;

        onOpened?.Invoke();
        Events.BoxOpened(_itemInstance);
    }

    private void OnItemDestroyed()
    {
        Saves.RemoveBoxes(_rarityType, 1);
        Destroy(gameObject);
        OpeningAvailable = true;
        Events.NewItemDestroyed();
    }


    private void OnItemPlaced()
    {
        _itemInstance.onPlaced -= OnItemPlaced;
        _itemInstance.onDestroyed -= OnItemDestroyed;
        OpeningAvailable = true;
        Saves.RemoveBoxes(_rarityType, 1);
        Destroy(gameObject);
        Events.NewItemPlaced();
    }

    private Item GetRandomItemPrefab()
    {
        while (true)
        {
            var allItemConfigs = Prefabs.GetAllItems();
            int randomIndex = UnityEngine.Random.Range(0, allItemConfigs.Length);

            var item = allItemConfigs[randomIndex];

            if (item.RarityExists(_rarityType))
                return item;
        }
    }



}
