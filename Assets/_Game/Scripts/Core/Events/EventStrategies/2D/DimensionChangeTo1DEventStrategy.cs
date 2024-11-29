public class DimensionChangeTo1DEventStrategy : IEventStrategy
{
    public void ApplyEvent()
    {
        SceneLoadManager.Instance.GoGame2DToGame1D();
    }

    public void StopEvent()
    {
        SceneLoadManager.Instance.GoGame1DToGame2D();
    }
}
