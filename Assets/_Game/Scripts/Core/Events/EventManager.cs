using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : MonoSingleton<EventManager>
{
    [SerializeField] private List<GameEvent> _eventsGeneral;
    [SerializeField] private List<GameEvent> _events3D;
    [SerializeField] private List<GameEvent> _events2D;
    [SerializeField] private List<GameEvent> _events1D;

    public List<GameEvent> Events;
    public float InputDelay;

    private List<GameEvent> _activePermanentEvents = new();
    public List<GameEvent> ActivePermanentEvents => _activePermanentEvents;
    private GameEvent _currentEvent;

    public event System.Action OnPermanentEventAdded;
    public event System.Action OnPermanentEventRemoved;

    private void Awake()
    {
        Events.AddRange(_eventsGeneral);
        Events.AddRange(_events3D);
        Events.AddRange(_events2D);
        Events.AddRange(_events1D);
    }

    public void ChooseEvent()
    {
        List<GameEvent> validEvents = new(_eventsGeneral);
        validEvents.RemoveAll(validEvent => _activePermanentEvents.Contains(validEvent));

        if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene))
        {
            validEvents.AddRange(_events3D);
        }
        else if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene2D))
        {
            validEvents.AddRange(_events2D);
        }
        else if (SceneLoadManager.Instance.IsSceneLoaded(SceneLoader.Scenes.GameScene1D))
        {
            validEvents.AddRange(_events1D);
        }


        int randomEventIndex = Random.Range(0, validEvents.Count);
        validEvents[randomEventIndex].ApplyEvent();
        _currentEvent = validEvents[randomEventIndex];
        if (_currentEvent.Permanent)
        {
            _activePermanentEvents.Add(_currentEvent);
            OnPermanentEventAdded?.Invoke();
        }
    }

    public void StopCurrentEvent()
    {
        _currentEvent.StopEvent();
    }

    public void StopPermanentEvent(GameEvent gameEvent)
    {
        if (_activePermanentEvents.Contains(gameEvent))
        {
            gameEvent.StopEvent();
            _activePermanentEvents.Remove(gameEvent);
            OnPermanentEventRemoved?.Invoke();
        }
    }
}
