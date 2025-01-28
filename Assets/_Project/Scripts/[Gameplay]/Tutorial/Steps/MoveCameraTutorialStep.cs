using R3;
using UnityEngine;

public class MoveCameraTutorialStep : TutorialStep
{
    private CameraController _cameraController;
    private GameObject _cursor;
    private GameObject _prompt;
    private ProgressMarker _progressMarker;
    private float _currentMove = 0f;

    private const float MOVE_REQUIRE = 3f;


    public MoveCameraTutorialStep(CameraController cameraController)
    {
        _cameraController = cameraController;
    }


    public override void RestoreContext()
    {
        base.RestoreContext();

        if (_cursor != null) Object.Destroy(_cursor);
        if (_prompt != null) Object.Destroy(_prompt);
    }


    public override void Run()
    {
        Disposables.Add(_cameraController.OnCameraMoved.Subscribe(move => OnCameraMoved(move)));

        TaskController.SetTask(0);

        var cursorTween = Object.Instantiate(Dependencies.PressAndFadeMoveCursorPrefab, UI.Canvas);
        var promptText = Object.Instantiate(Dependencies.LeftPromptPrefab, UI.Canvas);
        _progressMarker = Object.Instantiate(Dependencies.ProgressMarkerPrefab, UI.Canvas);
        _progressMarker.SetProgress(0f);


        Vector2 screenCenter = ScreenCalculator.GetScreenCenter();
        Vector2 offset = Vector2.up * 100f;

        Vector2 from = screenCenter + new Vector2(200f, 200f) + offset;
        promptText.transform.position = from;
        promptText.text = "Проведи по экрану,\nчтобы переместиться";

        _progressMarker.transform.position = screenCenter;


        Vector2 to = screenCenter + new Vector2(-200f, -200f) + offset;

        cursorTween.Run(from, to);

        _cursor = cursorTween.gameObject;
        _prompt = promptText.gameObject;


    }

    private void OnCameraMoved(Vector2 move)
    {
        _cursor.SetActive(false);
        _prompt.SetActive(false);


        _currentMove += move.magnitude;
        _progressMarker.SetProgress(_currentMove / MOVE_REQUIRE);

        if (_currentMove > MOVE_REQUIRE)
        {
            _progressMarker.Done();
            StepCompleted();
        }
    }

}
