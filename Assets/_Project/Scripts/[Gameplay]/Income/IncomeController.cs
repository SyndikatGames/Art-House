using System;
using UnityEngine;
using VG2;
using Zenject;

public class IncomeController : ITickable, IOfflineTimeHandler, IInitializable
{
    private static bool _startWindowWasShown = false;


    public TimeSpan LastPassedOfflineTime { get; private set; }

    public float TimeToPutIncomeLeft { get; private set; } = ConfigHub.Income.PutIncomeEverySeconds;

    public DateTime LastHandledTime 
    { 
        get => GameState.lastIncomeTime;
        set => GameState.lastIncomeTime = value;
    }

    private IncomeWindowView _incomeWindow;



    public void CollectCurrentIncome()
    {
        GameState.money.Value += GameState.CurrentRoom.accumulatedMoney.Value;
        GameState.CurrentRoom.accumulatedMoney.Value = 0f;
    }

    public void CollectOfflineIncome(float multiplier)
    {
        GameState.money.Value += GameState.CurrentRoom.accumulatedMoney.Value * multiplier;
        GameState.CurrentRoom.accumulatedMoney.Value = 0f;

        UnityEngine.Object.Destroy(_incomeWindow.gameObject);
    }



    public void HandlePassedOfflineTime(TimeSpan time)
    {
        LastPassedOfflineTime = time;

        float totalHours = Mathf.Min((float)time.TotalHours, ConfigHub.BaseValues.MaxOfflineHours);

        int roomLevel = PrestigeCalculator.GetCurrentRoomLevel();
        GameState.CurrentRoom.accumulatedMoney.Value += 
            (float)(ConfigHub.Income.GetIncomePerHour(roomLevel) * totalHours);
    }

    public void Initialize()
    {
        ShowStartIncomeWindow();
    }

    private void ShowStartIncomeWindow()
    {
        if (!_startWindowWasShown && GameState.tutorialCompleted)
        {
            _incomeWindow = SceneContainer.InstantiatePrefabFromComponent(ConfigHub.Income.IncomeWindow, UI.Canvas);
            _startWindowWasShown = true;
        }
    }


    public void Tick()
    {
        TimeToPutIncomeLeft -= Time.deltaTime;
        if (TimeToPutIncomeLeft <= 0f)
        {
            PutIncome();
            TimeToPutIncomeLeft = ConfigHub.Income.PutIncomeEverySeconds;
        }
    }



    private void PutIncome()
    {
        int roomLevel = PrestigeCalculator.GetCurrentRoomLevel();
        float passedSeconds = ConfigHub.Income.PutIncomeEverySeconds;

        float income = ConfigHub.Income.GetIncomePerHour(roomLevel) / 3600f * passedSeconds;

        GameState.CurrentRoom.accumulatedMoney.Value += income;
    }



}
