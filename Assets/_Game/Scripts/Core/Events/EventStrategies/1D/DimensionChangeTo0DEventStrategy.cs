public class DimensionChangeTo0DEventStrategy : IEventStrategy
{
    public void ApplyEvent()
    {
        SceneLoadManager.Instance.GoGame1DToGame0D();
    }

    public void StopEvent()
    {
        
    }
}
