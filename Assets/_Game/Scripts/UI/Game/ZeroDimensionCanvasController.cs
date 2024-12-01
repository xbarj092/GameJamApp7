using System.Collections;
using UnityEngine;

public class ZeroDimensionCanvasController : BaseCanvasController
{
    [SerializeField] private DeathScreen _deathScreen;
    [SerializeField] private float _deathScreenDelay = 2.0f;

    private GameScreen _cachedDeathScreen;

    protected override GameScreen GetRelevantScreen(GameScreenType gameScreenType)
    {
        if (gameScreenType == GameScreenType.Death)
        {
            StartCoroutine(DelayedDeathScreenInstantiation());
            return null;
        }

        return base.GetRelevantScreen(gameScreenType);
    }

    private IEnumerator DelayedDeathScreenInstantiation()
    {
        yield return new WaitForSeconds(_deathScreenDelay);
        if (_cachedDeathScreen == null)
        {
            _cachedDeathScreen = Instantiate(_deathScreen, transform);
        }

        _cachedDeathScreen.Open();

        _instantiatedScreens[GameScreenType.Death] = _cachedDeathScreen;
        ScreenManager.Instance.SetActiveGameScreen(_cachedDeathScreen);
    }
}
