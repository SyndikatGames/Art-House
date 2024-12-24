using UnityEngine;
using UnityEngine.SceneManagement;
using VG2;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject] private UnboxingController _unboxingController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameState.money.Value++;
            SceneManager.LoadScene(1);
        }
    }
}
