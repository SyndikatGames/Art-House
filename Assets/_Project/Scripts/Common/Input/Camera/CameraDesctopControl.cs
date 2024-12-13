using UnityEngine;


public class CameraDesctopControl : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _scrollSpeed = 1.2f;
    [SerializeField] private Vector2 _minMaxOrthographicSize;

    private void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A è D
        float vertical = Input.GetAxis("Vertical"); // W è S

        Vector3 movement = new Vector3(horizontal, vertical, 0) * _moveSpeed * Time.deltaTime;

        transform.Translate(movement);
    }

    private void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Camera camera = GetComponent<Camera>();
        camera.orthographicSize -= scroll * _scrollSpeed;

        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 
            _minMaxOrthographicSize.x, _minMaxOrthographicSize.y);
    }
}
