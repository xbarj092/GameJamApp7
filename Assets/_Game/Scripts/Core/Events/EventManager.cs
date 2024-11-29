using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : MonoSingleton<EventManager>
{
    [SerializeField] private List<GameEvent> _eventsGeneral;
    [SerializeField] private List<GameEvent> _events3D;
    [SerializeField] private List<GameEvent> _events2D;
    [SerializeField] private List<GameEvent> _events1D;

    public void ChooseEvent()
    {
        List<GameEvent> validEvents = _eventsGeneral;

        if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene))
        {
            validEvents.Concat(_events3D);
        }
        else if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.Game2DScene))
        {
            validEvents.Concat(_events2D);
        }
        else if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.Game1DScene))
        {
            validEvents.Concat(_events1D);
        }

        int randomEventIndex = Random.Range(0, validEvents.Count);
        validEvents[randomEventIndex].ApplyEvent();
    }
}
