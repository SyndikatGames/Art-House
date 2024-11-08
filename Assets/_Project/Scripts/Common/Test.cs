using UnityEngine;
using VG;

public class Test : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            print(Saves.GetBoxesAmount());
        }
    }
}
