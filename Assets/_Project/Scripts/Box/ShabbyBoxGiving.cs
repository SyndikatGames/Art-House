using UnityEngine;
using VG;

public class ShabbyBoxGiving : MonoBehaviour
{
    [SerializeField] private Item _item;


    private void OnEnable()
    {
        _item.onPlaced += OnItemPlaced;
    }

    private void OnDisable()
    {
        _item.onPlaced -= OnItemPlaced;
    }

    private void OnItemPlaced()
    {
        if (Saves.GetBoxesAmount() == 0)
            Saves.AddBoxes(RarityType.Shabby, 1);
    }

    

}
