using R3;
using UnityEngine;

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
    }


    public override void Run()
    {
        Disposables.Add(_cameraController.OnCameraScaled.Subscribe(move => OnCameraScaled(move)));

        TaskController.SetTask(0);

        var promptText = Object.Instantiate(Dependencies.BigCenterPromptPrefab, UI.Canvas);
        promptText.transform.position = Dependencies.InventoryRect.position + Vector3.up * 140f;
        promptText.text = "Используй колесико мыши, чтобы изменить масштаб";

        _progressMarker = Object.Instantiate(Dependencies.ProgressMarkerPrefab, UI.Canvas);
        _progressMarker.SetProgress(0f);

        _progressMarker.transform.position = ScreenCalculator.GetScreenCenter();

        _prompt = promptText.gameObject;


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
