using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (var item in ItemList.Items)
            {
                print($"{item.name}: {item.Position} {item.Position + item.Size}");
            }
        }
    }
}
