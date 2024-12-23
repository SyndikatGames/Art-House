using UnityEngine;
using UnityEngine.SceneManagement;
using VG2;
using Zenject;

public class Test : MonoBehaviour
{
    [Inject] private UnboxingController _unboxingController;
    [Inject] private GameState _gameState;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            print(_gameState.money);
            _gameState.money.Value++;
            SceneManager.LoadScene(1);
        }
    }
}
