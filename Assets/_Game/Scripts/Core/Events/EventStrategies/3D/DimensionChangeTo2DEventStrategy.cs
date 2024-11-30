using System.Linq;

public class DimensionChangeTo2DEventStrategy : IEventStrategy
{
    public void ApplyEvent()
    {
        if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene1D))
        {
            // EventManager.Instance.StopPermanentEvent(EventManager.Instance.Events.First(gameEvent => gameEvent.GameEventType == GameEventType.DimensionChangeTo1D));
            SceneLoadManager.Instance.GoGame1DToGame2D();
        }
        else if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene))
        {
            SceneLoadManager.Instance.GoGameToGame2D();
        }
    }

    public void StopEvent()
    {
        SceneLoadManager.Instance.GoGame2DToGame();
    }
}
