using UnityEngine;
using VG;

public class CameraMobileControl : MonoBehaviour
{
    public static bool MovingNow { get; private set; } = false;

    [SerializeField] private float _scaleSensitivity = 1f;
    [SerializeField] private Vector2 _minMaxOrthographicSize;
    [SerializeField] private HoldingButton _upScaleButton;
    [SerializeField] private HoldingButton _downScaleButton;

    private Vector2 _touchStartPosition;
    private float _startTouchesDistance;
    private Camera _camera;
    private bool _movingEnabled = true;


    private void Start()
    {
        _camera = Camera.main;
        Input.multiTouchEnabled = false;

        _upScaleButton.onBeginHolding += OnMobileButtonBeginHolding;
        _upScaleButton.onHolding += OnUpScaleButtonHolding;
        _downScaleButton.onHolding += OnDownScaleButtonHolding;

        if (DeviceInfo.DeviceType == VG.DeviceType.Desktop)
        {
            _upScaleButton.gameObject.SetActive(false);
            _downScaleButton.gameObject.SetActive(false);
        }

    }

    private void OnMobileButtonBeginHolding()
    {
        throw new System.NotImplementedException();
    }

    private void OnDownScaleButtonHolding()
    {
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize + _scaleSensitivity * Time.deltaTime,
            _minMaxOrthographicSize.x, _minMaxOrthographicSize.y);
    }

    private void OnUpScaleButtonHolding()
    {
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - _scaleSensitivity * Time.deltaTime,
            _minMaxOrthographicSize.x, _minMaxOrthographicSize.y);
    }



    private void Update()
    {
        if (ItemMoveHandler.DraggableItem != null) return;

        if (Input.touchCount == 1 && _movingEnabled)
            HandleMoving();
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

                print($"Moved: start: {_touchStartPosition}, current {currentPosition}");
                Vector2 positionDifference = currentPosition - _touchStartPosition;

                transform.position -= (Vector3)positionDifference;
                break;

        }
    }




}
