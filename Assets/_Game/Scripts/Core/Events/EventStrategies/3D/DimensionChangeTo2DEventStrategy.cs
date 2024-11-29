public class DimensionChangeTo2DEventStrategy : IEventStrategy
{
    public void ApplyEvent()
    {
        SceneLoadManager.Instance.GoGameToGame2D();
    }

    public void StopEvent()
    {
        SceneLoadManager.Instance.GoGame2DToGame();
    }
}
