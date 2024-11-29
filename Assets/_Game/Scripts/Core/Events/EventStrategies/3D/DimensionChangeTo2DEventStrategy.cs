public class DimensionChangeTo2DEventStrategy : IEventStrategy
{
    public void ApplyEvent()
    {
        SceneLoadManager.Instance.GoGameToGame2D();
    }
}
