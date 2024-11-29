using UnityEngine;

public class CameraMobileControl : MonoBehaviour
{
    [SerializeField] private float swipeSensitivity = 1f;

    private Vector3 touchStart;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }




    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = _camera.ScreenToWorldPoint(touch.position);
                    break;

                case TouchPhase.Moved:
                    Vector3 touchPosition = _camera.ScreenToWorldPoint(touch.position);
                    Vector3 swipeDelta = touchPosition - touchStart;

                    transform.position -= new Vector3(swipeDelta.x, swipeDelta.y, 0) * swipeSensitivity;

                    touchStart = touchPosition;
                    break;
            }


            // Обработка масштабирования с помощью двух пальцев
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                // Определение положения пальцев
                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
                Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

                // Вычисляем длину между пальцами в предыдущем и текущем кадре
                float prevTouchDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
                float touchDeltaMag = (touch1.position - touch2.position).magnitude;

                // Разница в длине
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // Масштабируем камеру
                _camera.orthographicSize += deltaMagnitudeDiff * 0.1f; // Уменьшите или увеличьте множитель для настройки чувствительности
                _camera.orthographicSize = Mathf.Max(_camera.orthographicSize, 0.1f); // Ограничиваем минимальный размер
            }


        }
    }
}
