using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public GameEventType GameEventType;
    public bool Permanent;
    public Sprite Sprite;

    private IEventStrategy _strategy;
    private EventStrategyFactory _strategyFactory;

    private void Start()
    {
        _strategyFactory = new();
        _strategy = _strategyFactory.CreateEventStrategy(GameEventType);
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
