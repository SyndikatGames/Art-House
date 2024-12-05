using UnityEngine;

public class CameraMobileControl : MonoBehaviour
{
    [SerializeField] private float _swipeSensitivity = 1f;
    [SerializeField] private float _scaleSensitivity = 1f;
    [SerializeField] private Vector2 _minMaxOrthographicSize;

    private const float sensitivityCoef = 0.001f;
    private const float cameraScaleAndSwipeSensitivityDependence = 0.5f;


    private Vector3 touchStart;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }




    private void Update()
    {
        if (ItemMoveHandler.DraggableItem != null) return;

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 swipeDelta = touch.deltaPosition;
                float totalCoef = (_swipeSensitivity + _camera.orthographicSize 
                    * cameraScaleAndSwipeSensitivityDependence) * sensitivityCoef;

                transform.position -= new Vector3(swipeDelta.x, swipeDelta.y, 0) * totalCoef;
            }
        }

        // Обработка масштабирования с помощью двух пальцев
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

            float previousTouchDeltaMagnitude = (touch1PrevPos - touch2PrevPos).magnitude;
            float currentTouchDeltaMagnitude = (touch1.position - touch2.position).magnitude;

            float deltaMagnitudeDifference = previousTouchDeltaMagnitude - currentTouchDeltaMagnitude;

            _camera.orthographicSize += deltaMagnitudeDifference * _scaleSensitivity * sensitivityCoef;
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 
                _minMaxOrthographicSize.x, _minMaxOrthographicSize.y);
        }



    }
}
