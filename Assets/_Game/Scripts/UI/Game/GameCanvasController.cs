using UnityEngine;

public class GameCanvasController : BaseCanvasController
{
    [SerializeField] private DeathScreen _deathScreenPrefab;
    [SerializeField] private PauseScreen _pauseScreenPrefab;
    [SerializeField] private RepairScreen _repairScreenPrefab;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        return gameScreenType switch
        {
            GameScreenType.Death => Instantiate(_deathScreenPrefab, transform),
            GameScreenType.Pause => Instantiate(_pauseScreenPrefab, transform),
            GameScreenType.Repair => Instantiate(_repairScreenPrefab, transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }
}
