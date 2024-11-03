using System.Collections.Generic;
using UnityEngine;

public class BoxConfig : MonoBehaviour
{
    [SerializeField] private List<Item> _items;


    public Item GetRandomItem()
    {
        int randomIndex = Random.Range(0, _items.Count);
        return _items[randomIndex];
    }


}
