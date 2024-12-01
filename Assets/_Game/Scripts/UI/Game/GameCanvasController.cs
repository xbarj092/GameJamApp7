using UnityEngine;

public class GameCanvasController : BaseCanvasController
{
    [SerializeField] private DeathScreen _deathScreenPrefab;
    [SerializeField] private PauseScreen _pauseScreenPrefab;
    [SerializeField] private RepairScreen _repairScreenPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ScreenManager.Instance.ActiveGameScreen != null)
            {
                Time.timeScale = 1;
                ScreenEvents.OnGameScreenClosedInvoke(GameScreenType.Pause);
            }
            else
            {
                Time.timeScale = 0;
                ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Pause);
            }
        }
    }

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
