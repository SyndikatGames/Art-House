using UnityEngine;
using DG.Tweening;
using System;
using VG;

public class Box : MonoBehaviour
{
    public event Action onOpened;

    [SerializeField] private RarityType _rarityType;

    private Item _itemInstance;
    private Vector2 _originPosition;

    private const float itemMoveY = 0.5f;
    private const float showDuration = 0.5f;

    private void Start()
    {
        _originPosition = transform.position;
    }

    


    public void Open()
    {
        Item itemPrefab = GetRandomItemPrefab();
        _itemInstance = Instantiate(itemPrefab, _originPosition, Quaternion.identity);
        _itemInstance.SetRarity(_rarityType);

        _itemInstance.transform.DOMoveY(_originPosition.y + itemMoveY, showDuration);

        _itemInstance.transform.localScale = Vector3.zero;
        _itemInstance.transform.DOScale(1f, showDuration);

        transform.DOMoveY(_originPosition.y - itemMoveY, showDuration);
        transform.DOScale(0f, showDuration)
            .onComplete += () => Destroy(gameObject);

        _itemInstance.onPlaced += OnItemPlaced;

        onOpened?.Invoke();
        Saves.RemoveBoxes(_rarityType, 1);
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


    private void OnItemPlaced()
    {
        _itemInstance.onPlaced -= OnItemPlaced;
        //Show();

    }

    /*
    public void Show()
    {
        transform.DOMoveY(_originPosition.y, showDuration);
        transform.DOScale(1f, showDuration);
    }
    */

}
