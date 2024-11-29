using UnityEngine;

public class GameEvent : MonoBehaviour
{
    [SerializeField] private GameEventType _gameEventType;

    public bool Permanent;

    private IEventStrategy _strategy;
    private EventStrategyFactory _strategyFactory;

    private void Start()
    {
        _strategyFactory = new();
        _strategy = _strategyFactory.CreateEventStrategy(_gameEventType);
    }

    public void ApplyEvent()
    {
        _strategy.ApplyEvent();
    }

    public void StopEvent()
    {
        _strategy.StopEvent();
    }
}
