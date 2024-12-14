using UnityEngine;
using VG;

public class CameraMobileControl : MonoBehaviour
{
    [SerializeField] private float _moveSensitivity = 0.5f;
    [SerializeField] private float _scaleSensitivity = 1f;
    [SerializeField] private Vector2 _minMaxOrthographicSize;
    [Space(10)]
    [SerializeField] private HoldingButton _upScaleButton;
    [SerializeField] private HoldingButton _downScaleButton;

    private Vector2 _touchStartPosition;
    private Camera _camera;

    private const float speedMoveThresholdCoefficient = 1f;

    public float SpeedMoveThreshold => _camera.orthographicSize * speedMoveThresholdCoefficient;

    private void Start()
    {
        _camera = Camera.main;

        if (DeviceInfo.ControlType == ControlType.Mobile)
        {
            _upScaleButton.onBeginHolding += OnBeginHoldingScaleButton;
            _upScaleButton.onHolding += OnHoldingUpScaleButton;
            _upScaleButton.onEndHolding += OnEndHoldingScaleButton;

            _downScaleButton.onBeginHolding += OnBeginHoldingScaleButton;
            _downScaleButton.onHolding += OnHoldingDownScaleButton;
            _downScaleButton.onEndHolding += OnEndHoldingScaleButton;
        }
        else
        {
            enabled = false;
            _upScaleButton.gameObject.SetActive(false);
            _downScaleButton.gameObject.SetActive(false);
        }

    }

    private void OnBeginHoldingScaleButton() => MobileControl.CurrentState = MobileControl.State.CameraScaling;

    private void OnHoldingUpScaleButton() => ChangeCameraScale(+1);
    private void OnHoldingDownScaleButton() => ChangeCameraScale(-1);

    private void OnEndHoldingScaleButton() => MobileControl.CurrentState = MobileControl.State.Free;

    



    private void ChangeCameraScale(float value)
    {
        _camera.orthographicSize = Mathf.Clamp
            (_camera.orthographicSize + value * _scaleSensitivity * Time.deltaTime,
            _minMaxOrthographicSize.x, _minMaxOrthographicSize.y);
    }


    private void Update()
    {
        if (Input.touchCount > 0)
            UpdateMobileControlState();

        if (MobileControl.CurrentState == MobileControl.State.CameraMoving)
            HandleMoving();
    }

    private void UpdateMobileControlState()
    {
        if (MobileControl.CurrentState == MobileControl.State.Free)
        {
            var freeTouch = Input.GetTouch(0);

            if (freeTouch.phase == TouchPhase.Moved)
            {
                Vector2 previousPosition = _camera.ScreenToWorldPoint(freeTouch.position - freeTouch.deltaPosition);
                Vector2 currentPosition = _camera.ScreenToWorldPoint(freeTouch.position);
                ScreenLogger.Log($"Prev: {previousPosition}, Curr: {currentPosition}");

                float changeSpeed = Vector2.Distance(previousPosition, currentPosition) / freeTouch.deltaTime;
                bool touchForCamera = changeSpeed > SpeedMoveThreshold;

                ScreenLogger.Log($"Compare: {changeSpeed} {SpeedMoveThreshold}");
                if (touchForCamera) MobileControl.CurrentState = MobileControl.State.CameraMoving;
            }
        }
        

        if (MobileControl.CurrentState == MobileControl.State.CameraMoving)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                MobileControl.CurrentState = MobileControl.State.Free;
        }


    }


    private void HandleMoving()
    {
        var touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                _touchStartPosition = _camera.ScreenToWorldPoint(touch.position);
                break;

            case TouchPhase.Moved:
                Vector2 previousPosition = _camera.ScreenToWorldPoint(touch.position - touch.deltaPosition);
                Vector2 currentPosition = _camera.ScreenToWorldPoint(touch.position);

                Vector2 positionDifference = currentPosition - previousPosition;

                if (Environment.platform == Environment.Platform.WebGL)
                    transform.position += (Vector3)positionDifference * _moveSensitivity;

                else transform.position -= (Vector3)positionDifference * _moveSensitivity;

                break;

        }
    }




}
