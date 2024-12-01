using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int CurrentLevel;
    public int MaxLevel;

    public bool Paused;

    public float TimeToNextRepair;

    public event Action OnLevelChanged;
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
            MaxLevel = CurrentLevel;
            OnLevelChanged?.Invoke();
            EventManager.Instance.ChooseEvent();
        }
        else if (CurrentLevel < MaxLevel)
        {
            CurrentLevel = MaxLevel;
        }
    }

    public void DecreaseLevel()
    {
        CurrentLevel--;
    }

    public void ResetScript()
    {
        TimeToNextRepair = 0;
        CurrentLevel = 1;
        MaxLevel = 1;
    }

    public void Pause()
    {
        Paused = true;
        Time.timeScale = 0;
        AudioManager.Instance.Pause();
    }

    public void Unpause()
    {
        Paused = false;
        Time.timeScale = 1;
        AudioManager.Instance.Unpause();
    }
}
