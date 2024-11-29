using UnityEngine;
using VG;

public class EarnShabbyBox_Event : MonoBehaviour
{

    private void OnEnable()
    {
        Saves.String[Key_Save.boxes_data(0)].onChanged += OnBoxesChanged;
    }

    private void OnDisable()
    {
        Saves.String[Key_Save.boxes_data(0)].onChanged -= OnBoxesChanged;
    }

    private void OnBoxesChanged()
    {
        if (Saves.GetNormalBoxesAmount() == 0 
            && Saves.GetBoxes(RarityType.Shabby) == 0 
            && Saves.Float[Key_Save.random_boxes(0)].Value < 1f)
        {
            Saves.AddBoxes(RarityType.Shabby, 1);
        }
    }


}
