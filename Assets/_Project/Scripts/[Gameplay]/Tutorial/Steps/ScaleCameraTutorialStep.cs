using R3;
using UnityEngine;
using VG2;

public class ScaleCameraTutorialStep : TutorialStep
{
    private CameraController _cameraController;
    private GameObject _prompt;
    private ProgressMarker _progressMarker;
    private float _currentScale = 0f;

    private const float SCALE_REQUIRE = 1.2f;


    public ScaleCameraTutorialStep(CameraController cameraController)
    {
        _cameraController = cameraController;
    }


    public override void RestoreContext()
    {
        base.RestoreContext();
        if (_prompt != null) Object.Destroy(_prompt);
        RaycastBlock.Disable();
    }


    public override void Run()
    {
        Disposables.Add(_cameraController.OnCameraScaled.Subscribe(move => OnCameraScaled(move)));

        TaskController.SetTask(0);

        _progressMarker = Object.Instantiate(Dependencies.ProgressMarkerPrefab, UI.Canvas);
        _progressMarker.SetProgress(0f);
        _progressMarker.transform.position = ScreenCalculator.GetScreenCenter();

        RaycastBlock.BlockAll();

        if (DeviceInfo.ControlType == ControlType.Mobile)
        {
            var promptText = Object.Instantiate(Dependencies.LeftArrowPromptPrefab, UI.Canvas);
            promptText.transform.position = Dependencies.MobileControlRect.position;
            promptText.text = Localization.GetString("scale_tutorial_mobile");
            _prompt = promptText.gameObject;
        }

        else if (DeviceInfo.ControlType == ControlType.Desktop)
        {
            var promptText = Object.Instantiate(Dependencies.BigCenterPromptPrefab, UI.Canvas);
            promptText.transform.position = Dependencies.InventoryTopCenterPosition;
            promptText.text = Localization.GetString("scale_tutorial_desktop");
            _prompt = promptText.gameObject;
        }

    }

    private void OnCameraScaled(float scaleDelta)
    {
        _prompt.SetActive(false);

        _currentScale += Mathf.Abs(scaleDelta);
        _progressMarker.SetProgress(_currentScale / SCALE_REQUIRE);

        if (_currentScale > SCALE_REQUIRE)
        {
            _progressMarker.Done();
            StepCompleted();
        }
    }


}
