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
            Saves.Int[Key_Save.boxes_amount(RarityType.Shabby)].Value++;
    }

    

}
