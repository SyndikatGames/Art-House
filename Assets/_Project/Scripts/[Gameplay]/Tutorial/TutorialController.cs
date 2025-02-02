using System.Collections.Generic;
using PrizeClaw;
using UnityEngine;
using VG2;
using Zenject;

public class TutorialController : IInitializable
{
    private readonly List<TutorialStep> _steps;

    public const int PRIZE_CLAW_TUTORIAL_STEP_INDEX = 13;
    public const int SHOW_ROOM_STYLES_LEVEL = 3;

    public static bool ShowOfflineIncomeEnabled => GameState.tutorialStepIndex.Value >= 16;
    public static bool CameraControlEnabled => GameState.tutorialStepIndex.Value >= 3;
    public static bool SellAreaEnabled => GameState.tutorialStepIndex.Value >= 13;



    public TutorialController(TutorialSceneDependencies dependencies, PrestigeController prestigeController,
        EventController eventController, CameraController cameraController, TaskController taskController,
        ShopController shopController, UnboxingController unboxingController, GameController prizeClawController,
        RoomExpansionController roomExpansionController, RoomStyleController roomStyleController)
    {
        _steps = new List<TutorialStep>
        {
            new EnterCardsTutorialStep(eventController, highlightPlace: true), // 0
            new HoldAndMoveItemTutorialStep(eventController), // 1
            new RotateItemTutorialStep(eventController), // 2
            new MoveCameraTutorialStep(cameraController), // 3
            new ScaleCameraTutorialStep(cameraController), // 4
            new EnterCardsTutorialStep(eventController, highlightPlace: false), // 5
            new PrestigeHighlightTutorialStep(eventController), // 6
            new ReachNewLevelTutorialStep(eventController), // 7
            new TakeTaskRewardTutorialStep(0), // 8
            new OpenBoxesTutorialStep(shopController, unboxingController), // 9
            new TakeTaskRewardTutorialStep(1), // 10
            new MergeItemsTutorialStep(eventController), // 11
            new TakeTaskRewardTutorialStep(2), // 12
            new PlayPrizeClawTutorialStep(prizeClawController), // 13
            new TakeTaskRewardTutorialStep(3), // 14
            new ExpandRoomTutorialStep(roomExpansionController), // 15
            new UseNewStyleTutorialStep(eventController, roomStyleController), // 16
        };


        foreach (var step in _steps)
            step.SetDependecies(dependencies, taskController);
    }

    public void Initialize()
    {
        if (GameState.tutorialStepIndex.Value < _steps.Count) 
            RunStep(GameState.tutorialStepIndex.Value, usePreviousContext: false);
    }


    private void RunStep(int stepIndex, bool usePreviousContext)
    {
        Debug.Log($"Run: {stepIndex}");

        if (usePreviousContext) 
            _steps[stepIndex - 1].RestoreContext();

        _steps[stepIndex].Run();
        _steps[stepIndex].onCompleted += () =>
        {
            Debug.Log($"Completed: {stepIndex}");
            int finishedStepIndex = stepIndex;

            GameState.tutorialStepIndex.Value++;

            if (finishedStepIndex < _steps.Count - 1)
                RunStep(finishedStepIndex + 1, usePreviousContext: true);

            else _steps[finishedStepIndex].RestoreContext();
        };

    }

    
}
