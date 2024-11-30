using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image _progressBarFill;

    private const float TIME_FOR_REPAIR = 10f;

    private void Awake()
    {
        StartTimeToNextRepair();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnRepairTimerReset += StartTimeToNextRepair;
        EventManager.Instance.OnPermanentEventAdded += StartTimeToNextRepair;
        EventManager.Instance.OnPermanentEventRemoved += StopAllCoroutines;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnRepairTimerReset -= StartTimeToNextRepair;
        EventManager.Instance.OnPermanentEventAdded -= StartTimeToNextRepair;
        EventManager.Instance.OnPermanentEventRemoved -= StopAllCoroutines;

        StopAllCoroutines();
    }

    public void StartTimeToNextRepair()
    {
        StartCoroutine(StartTimeout());
    }

    private IEnumerator StartTimeout()
    {
        if (EventManager.Instance.ActivePermanentEvents.Count == 0)
        {
            yield break;
        }

        while (GameManager.Instance.TimeToNextRepair < TIME_FOR_REPAIR)
        {
            GameManager.Instance.TimeToNextRepair += Time.deltaTime;
            _progressBarFill.fillAmount = 1 - (GameManager.Instance.TimeToNextRepair / TIME_FOR_REPAIR);
            yield return null;
        }

        GameManager.Instance.TimeToNextRepair = 0;
        _progressBarFill.fillAmount = 1f;
        OnRepairCompletedInvoke();
    }

    private void OnRepairCompletedInvoke()
    {
        ScreenEvents.OnGameScreenOpenedInvoke(GameScreenType.Repair);
    }
}
