using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image _progressBarFill;
    [SerializeField] private List<TMP_Text> _keyTexts;

    private const float TIME_FOR_REPAIR = 10f;

    private void Awake()
    {
        OnInputChanged(EventManager.Instance.CurrentBinding);
        StartTimeToNextRepair();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnRepairTimerReset += StartTimeToNextRepair;
        EventManager.Instance.OnPermanentEventAdded += StartTimeToNextRepair;
        EventManager.Instance.OnPermanentEventRemoved += StopAllCoroutines;
        EventManager.Instance.OnInputChanged += OnInputChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnRepairTimerReset -= StartTimeToNextRepair;
        EventManager.Instance.OnPermanentEventAdded -= StartTimeToNextRepair;
        EventManager.Instance.OnPermanentEventRemoved -= StopAllCoroutines;
        EventManager.Instance.OnInputChanged -= OnInputChanged;

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
}
