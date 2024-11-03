using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Project/Box", fileName = "Box")]

public class BoxConfig : ScriptableObject
{
    [SerializeField] private List<Item> _items;


    public Item GetRandomItem()
    {
        int randomIndex = Random.Range(0, _items.Count);
        return _items[randomIndex];
    }


}
