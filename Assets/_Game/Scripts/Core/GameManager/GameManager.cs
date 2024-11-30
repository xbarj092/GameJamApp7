using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int CurrentLevel;
    public int MaxLevel;

    public float TimeToNextRepair;

    public event Action OnRepairTimerReset;
    public void OnRepairTimerResetInvoke()
    {
        OnRepairTimerReset?.Invoke();
    }

    public void IncreaseLevel()
    {
        CurrentLevel++;
        if (CurrentLevel > MaxLevel)
        {
            CurrentLevel = MaxLevel;
            EventManager.Instance.ChooseEvent();
        }
        else if (CurrentLevel + 1 < MaxLevel)
        {
            CurrentLevel = MaxLevel;
        }
    }

    public void DecreaseLevel()
    {
        CurrentLevel--;
    }
}
