using System.Collections;
using UnityEngine;

public class TimedLevelEventStrategy : IEventStrategy
{
    private const float BASE_TIME_TO_DISINTEGRATION = 20;

    private static DummyMonoBehaviour _loaderMonoBehaviour;
    private static DummyMonoBehaviour LoaderMonoBehaviour
    {
        get
        {
            if (_loaderMonoBehaviour == null)
            {
                GameObject loaderGameObject = new("Loader Game Object");
                _loaderMonoBehaviour = loaderGameObject.AddComponent<DummyMonoBehaviour>();
            }

            return _loaderMonoBehaviour;
        }
    }

    public void ApplyEvent()
    {
        TextManager.Instance.ShowText(StringStorageType.TimedLevel);
        LoaderMonoBehaviour.StartCoroutine(CountDownLevel());
    }

    private IEnumerator CountDownLevel()
    {
        float timeToLevelDisintegration = Random.Range(BASE_TIME_TO_DISINTEGRATION - 5, BASE_TIME_TO_DISINTEGRATION + 5);
        yield return new WaitUntil(() => TextManager.Instance.CurrentTextType != StringStorageType.TimedLevel);
        while (timeToLevelDisintegration > -1)
        {
            if (ScreenManager.Instance.ActiveGameScreen == null)
            {
                timeToLevelDisintegration -= Time.deltaTime;
                EventManager.Instance.TimeToLevelDisintegration = timeToLevelDisintegration;
            }

            yield return null;
        }

        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Death);
    }

    public void StopEvent()
    {
        LoaderMonoBehaviour.StopAllCoroutines();
    }
}
