public class DimensionChangeTo1DEventStrategy : IEventStrategy
{
    public void ApplyEvent()
    {
        if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene2D))
        {
            // EventManager.Instance.StopPermanentEvent(EventManager.Instance.Events.First(gameEvent => gameEvent.GameEventType == GameEventType.DimensionChangeTo2D));
            SceneLoadManager.Instance.GoGame2DToGame1D();
        }
    }

    public void StopEvent()
    {
        SceneLoadManager.Instance.GoGame1DToGame2D();
    }
}
