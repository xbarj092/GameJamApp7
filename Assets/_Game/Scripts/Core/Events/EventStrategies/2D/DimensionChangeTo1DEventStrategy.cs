using System.Linq;

public class DimensionChangeTo1DEventStrategy : IEventStrategy
{
    public void ApplyEvent()
    {
        if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene2D))
        {
            EventManager.Instance.StopPermanentEvent(EventManager.Instance.Events.First(gameEvent => gameEvent.GameEventType == GameEventType.DimensionChangeTo2D));
        }
    }

    public void StopEvent()
    {
        SceneLoadManager.Instance.GoGame1DToGame2D();
    }
}
