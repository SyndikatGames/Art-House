using UnityEngine;

public class RootItem : MonoBehaviour
{
    private static RootItem _instance;

    [SerializeField] private Item _item;


    private void Start()
    {
        _item.SetPlace(Vector3Int.zero, parent: null);
    }

}
