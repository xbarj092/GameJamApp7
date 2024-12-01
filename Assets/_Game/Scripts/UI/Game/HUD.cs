using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image _progressBarFill;
    [SerializeField] private List<TMP_Text> _keyTexts;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private GameObject _hackingTimeout;

    private const float TIME_FOR_REPAIR = 100f;
    private const string DISINTEGRATION_TIME = "Time to disintegration: ";

    private void Start()
    {
        _hackingTimeout.SetActive(TextManager.Instance.HasPlayedTutorial(StringStorageType.HackingTimeout));

        OnInputChanged(EventManager.Instance.CurrentBinding);
        StartTimeToNextRepair();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnRepairTimerReset += StartTimeToNextRepair;
        GameManager.Instance.OnLevelChanged += OnLevelChanged;
        GameManager.Instance.OnTutorialFinished += OnTutorialFinished;

        EventManager.Instance.OnPermanentEventAdded += StartTimeToNextRepair;
        EventManager.Instance.OnPermanentEventRemoved += StopAllCoroutines;
        EventManager.Instance.OnInputChanged += OnInputChanged;
        EventManager.Instance.OnTimeToLevelDisintegrationChanged += OnTimeToDisintegrateChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnRepairTimerReset -= StartTimeToNextRepair;
        GameManager.Instance.OnLevelChanged -= OnLevelChanged;
        GameManager.Instance.OnTutorialFinished -= OnTutorialFinished;

        EventManager.Instance.OnPermanentEventAdded -= StartTimeToNextRepair;
        EventManager.Instance.OnPermanentEventRemoved -= StopAllCoroutines;
        EventManager.Instance.OnInputChanged -= OnInputChanged;
        EventManager.Instance.OnTimeToLevelDisintegrationChanged -= OnTimeToDisintegrateChanged;

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

    private void OnInputChanged(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            _keyTexts[i].text = list[i];
        }
    }

    private void OnTutorialFinished(StringStorageType type)
    {
        if (type != StringStorageType.HackingTimeout)
        {
            return;
        }

        _hackingTimeout.SetActive(true);
    }

    private void OnTimeToDisintegrateChanged(float secondsLeft)
    {
        if (!_timeText.isActiveAndEnabled)
        {
            _timeText.gameObject.SetActive(true);
        }

        _timeText.text = DISINTEGRATION_TIME + Mathf.CeilToInt(secondsLeft).ToString() + "s";
    }

    private void OnLevelChanged()
    {
        _timeText.gameObject.SetActive(false);
    }
}
