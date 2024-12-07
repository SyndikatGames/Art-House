using UnityEngine;

public class CameraMobileControl : MonoBehaviour
{
    [SerializeField] private float _scaleSensitivity = 1f;
    [SerializeField] private Vector2 _minMaxOrthographicSize;


    private Vector2 _touchStartPosition;
    private float _startTouchesDistance;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }


    private void Update()
    {
        if (ItemMoveHandler.DraggableItem != null) return;

        if (Input.touchCount == 1)
            HandleMoving();

        else if (Input.touchCount == 2)
            HandleScaling();

    }

    private void HandleMoving()
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                _touchStartPosition = _camera.ScreenToWorldPoint(touch.position);
                break;

            case TouchPhase.Moved:
                Vector2 currentPosition = _camera.ScreenToWorldPoint(touch.position);
                Vector2 positionDifference = currentPosition - _touchStartPosition;

                transform.position -= (Vector3)positionDifference;
                break;

        }
    }

    private void HandleScaling()
    {
        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        Vector2 touch1Position = _camera.ScreenToViewportPoint(touch1.position);
        Vector2 touch2Position = _camera.ScreenToViewportPoint(touch2.position);
        float currentTouchesDistance = Vector2.Distance(touch1Position, touch2Position);

        if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
        {
            _startTouchesDistance = currentTouchesDistance;
        }
        else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
        {
            float distanceDifference = currentTouchesDistance - _startTouchesDistance;
            print($"Start: {_startTouchesDistance}, current: {currentTouchesDistance}");
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - distanceDifference * _scaleSensitivity,
                _minMaxOrthographicSize.x, _minMaxOrthographicSize.y);
        }
    }



}
