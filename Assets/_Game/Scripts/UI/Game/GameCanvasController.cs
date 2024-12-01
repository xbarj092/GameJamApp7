using UnityEngine;

public class GameCanvasController : BaseCanvasController
{
    [SerializeField] private DeathScreen _deathScreenPrefab;
    [SerializeField] private PauseScreen _pauseScreenPrefab;
    [SerializeField] private RepairScreen _repairScreenPrefab;
    [SerializeField] private WinScreen _winScreenPrefab;

    private void Start()
    {
        TextManager.Instance.HandleCanvasSwitch();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ScreenManager.Instance.ActiveGameScreen?.GameScreenType != GameScreenType.Death)
        {
            if (GameManager.Instance.Paused)
            {
                GameManager.Instance.Unpause();
                ScreenEvents.OnGameScreenClosedInvoke(GameScreenType.Pause);
            }
            else
            {
                GameManager.Instance.Pause();
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
            GameScreenType.Win => Instantiate(_winScreenPrefab, transform),
            _ => base.GetRelevantScreen(gameScreenType),
        };
    }
}
