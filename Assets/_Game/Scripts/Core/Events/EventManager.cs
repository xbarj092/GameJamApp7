using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventManager : MonoSingleton<EventManager>
{
    [SerializeField] private List<GameEvent> _eventsGeneral;
    [SerializeField] private List<GameEvent> _events3D;
    [SerializeField] private List<GameEvent> _events2D;
    [SerializeField] private List<GameEvent> _events1D;

    public List<GameEvent> Events;
    public float InputDelay;
    public InputActionReference MovementAction;
    public List<string> CurrentBinding = new();
    private float _timeToLevelDisintegration;
    public float TimeToLevelDisintegration
    {
        get => _timeToLevelDisintegration;
        set
        {
            _timeToLevelDisintegration = value;
            OnTimeToLevelDisintegrationChanged?.Invoke(value);
        }
    }

    private List<GameEvent> _activePermanentEvents = new();
    public List<GameEvent> ActivePermanentEvents => _activePermanentEvents;
    private GameEvent _currentEvent;
    public GameEvent CurrentEvent => _currentEvent;

    public event System.Action<float> OnTimeToLevelDisintegrationChanged;
    public event System.Action OnPermanentEventAdded;
    public event System.Action OnPermanentEventRemoved;
    public event System.Action<List<string>> OnInputChanged;
    public void OnInputChangeInvoke(List<string> inputs)
    {
        CurrentBinding = inputs;
        OnInputChanged?.Invoke(inputs);
    }

    private void Awake()
    {
        Events.AddRange(_eventsGeneral);
        Events.AddRange(_events3D);
        Events.AddRange(_events2D);
        Events.AddRange(_events1D);
    }

    public void ChooseEvent()
    {
        StopCurrentEvent();
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
        if (validEvents[randomEventIndex].Permanent)
        {
            if (!TextManager.Instance.HasPlayedTutorial(StringStorageType.HackingTimeout))
            {
                TextManager.Instance.ShowText(StringStorageType.HackingTimeout, true);
                TextManager.Instance.CurrentText.OnTextFinished += OnTextFinished;
            }

            _activePermanentEvents.Add(validEvents[randomEventIndex]);
            OnPermanentEventAdded?.Invoke();
        }
        else
        {
            _currentEvent = validEvents[randomEventIndex];
        }
    }

    private void OnTextFinished()
    {
        TextManager.Instance.CurrentText.OnTextFinished -= OnTextFinished;
        GameManager.Instance.OnTutorialFinishedInvoke(StringStorageType.HackingTimeout);
    }

    private void StopCurrentEvent()
    {
        if (_currentEvent != null)
        {
            _currentEvent.StopEvent();
            _currentEvent = null;
        }
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

    public void ResetScript()
    {
        if (ActivePermanentEvents.Any(gameEvent => gameEvent.GameEventType == GameEventType.InputChange))
        {
            ActivePermanentEvents.First(gameEvent => gameEvent.GameEventType == GameEventType.InputChange).StopEvent();
        }
        _activePermanentEvents = new();
        _currentEvent = null;
        InputDelay = 0;
    }
}
