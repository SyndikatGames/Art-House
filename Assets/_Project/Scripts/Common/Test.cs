using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject] private UnboxingController _unboxingController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _unboxingController.RunUnboxing(BoxType.Common, 20);


        }
    }
}
