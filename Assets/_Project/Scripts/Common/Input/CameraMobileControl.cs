using UnityEngine;

public class CameraMobileControl : MonoBehaviour
{
    private Vector2 touchStart;
    private float swipeSensitivity = 0.1f;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }




    private void Update()
    {
        // Проверка наличия касания
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Сохраняем начальную позицию касания
                    touchStart = touch.position;
                    break;

                case TouchPhase.Moved:
                    // Подсчитываем текущее движение свайпа
                    Vector3 swipeDelta = touch.position - touchStart;

                    // Двигаем камеру по оси X и Y в зависимости от движения свайпа
                    transform.position += new Vector3(swipeDelta.x * swipeSensitivity, swipeDelta.y * swipeSensitivity, 0);

                    // Обновляем начальную позицию для следующего кадра
                    touchStart = touch.position;
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
