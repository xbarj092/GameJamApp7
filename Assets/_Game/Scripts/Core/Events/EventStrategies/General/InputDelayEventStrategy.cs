public class InputDelayEventStrategy : IEventStrategy
{
    public void ApplyEvent()
    {
        TextManager.Instance.ShowText(StringStorageType.InputDelay);
        EventManager.Instance.InputDelay = 0.5f;
    }

    public void StopEvent()
    {
        EventManager.Instance.InputDelay = 0f;
    }
}
