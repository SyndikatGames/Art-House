using UnityEngine;


public class CameraMoving : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _moveSpeed;


    private void Awake()
    {
        InputSystem.onMove += OnInputMove;
    }


    private void OnInputMove(Vector2 direction)
    {
        _camera.transform.Translate
            (direction * _moveSpeed * Time.deltaTime);



    }
}
