public class EventStrategyFactory
{
    public IEventStrategy CreateEventStrategy(GameEventType eventType)
    {
        return eventType switch
        {
            GameEventType.LanguageChange => new LanguageChangeEventStrategy(),
            GameEventType.InputChange => new InputChangeEventStrategy(),
            GameEventType.InputDelay => new InputDelayEventStrategy(),
            GameEventType.TimedLevel => new TimedLevelEventStrategy(),
            GameEventType.DimensionChangeTo0D => new DimensionChangeTo0DEventStrategy(),
            GameEventType.DimensionChangeTo1D => new DimensionChangeTo1DEventStrategy(),
            GameEventType.DimensionChangeTo2D => new DimensionChangeTo2DEventStrategy(),
            _ => null,
        };
        ;
    }
}
