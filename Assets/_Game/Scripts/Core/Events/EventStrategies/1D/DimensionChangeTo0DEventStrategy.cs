using System.Linq;

public class DimensionChangeTo0DEventStrategy : IEventStrategy
{
    public void ApplyEvent()
    {
        if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene1D))
        {
            EventManager.Instance.StopPermanentEvent(EventManager.Instance.Events.First(gameEvent => gameEvent.GameEventType == GameEventType.DimensionChangeTo1D));
        }
    }

    public void StopEvent()
    {
        
    }
}
