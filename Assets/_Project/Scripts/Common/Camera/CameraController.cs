using R3;
using UnityEngine;
using Zenject;

public class CameraController : ITickable
{
    public Observable<Vector2> OnCameraMoved => _onCameraMoved; private Subject<Vector2> _onCameraMoved = new Subject<Vector2>();
    public Observable<float> OnCameraScaled => _onCameraScaled; private Subject<float> _onCameraScaled = new Subject<float>();

    private float _previousScale;
    private Vector2 _previousPosition;
    private Camera _camera;

    public CameraController(Camera camera)
    {
        _camera = camera;
        _previousPosition = _camera.transform.position;
        _previousScale = _camera.orthographicSize;
    }

    public void Tick()
    {
        if (_previousPosition != (Vector2)_camera.transform.position)
        {
            Vector2 move = (Vector2)_camera.transform.position - _previousPosition;
            _onCameraMoved.OnNext(move);

            _previousPosition = _camera.transform.position;
        }

        if (_previousScale != _camera.orthographicSize)
        {
            float scaleChange = _camera.orthographicSize - _previousScale;
            _onCameraScaled.OnNext(scaleChange);
            _previousScale = _camera.orthographicSize;
        }
        
    }
}
