using System;

public class GameManager : MonoSingleton<GameManager>
{
    public int CurrentLevel;

    public float TimeToNextRepair;

    public event Action OnRepairTimerReset;
    public void OnRepairTimerResetInvoke()
    {
        OnRepairTimerReset?.Invoke();
    }
}
