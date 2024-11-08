using UnityEngine;


public class CameraMoving : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _scaleSpeed;


    private void Awake()
    {
        InputSystem.onMove += OnInputMove;
        InputSystem.onScaleChanged += OnScaleChanged;
    }

    private void OnDestroy()
    {
        InputSystem.onMove -= OnInputMove;
        InputSystem.onScaleChanged -= OnScaleChanged;
    }

    private void OnScaleChanged(float scaleDelta)
    {
        _camera.orthographicSize += _scaleSpeed * scaleDelta;
    }

    private void OnInputMove(Vector2 direction)
    {
        _camera.transform.Translate
            (direction * _moveSpeed * Time.deltaTime);



    }
}
