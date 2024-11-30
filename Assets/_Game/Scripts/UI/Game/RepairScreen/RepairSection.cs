using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairSection : MonoBehaviour
{
    [SerializeField] private Image _progressBarFill;
    [SerializeField] private List<Plug> _plugs;

    public event Action<bool> OnRepairComplete;

    private const float TIME_FOR_REPAIR = 20f;

    private void OnEnable()
    {
        foreach (Plug plug in _plugs)
        {
            plug.OnWireChanged += OnWireChanged;
        }
    }

    private void OnDisable()
    {
        foreach (Plug plug in _plugs)
        {
            plug.OnWireChanged -= OnWireChanged;
        }
    }

    public void StartRepairing()
    {
        SwapPlugChildren();
        StartCoroutine(StartTimeout());
    }

    private IEnumerator StartTimeout()
    {
        float elapsedTime = 0;
        while (elapsedTime < TIME_FOR_REPAIR)
        {
            elapsedTime += Time.deltaTime;
            _progressBarFill.fillAmount = 1 - (elapsedTime / TIME_FOR_REPAIR);
            yield return null;
        }

        _progressBarFill.fillAmount = 1f;
        OnRepairCompletedInvoke();
    }

    private void OnRepairCompletedInvoke()
    {
        OnRepairComplete?.Invoke(false);
    }

    private void OnWireChanged()
    {
        bool completed = true;
        foreach (Plug plug in _plugs)
        {
            DraggableItem draggableItem = plug.GetComponentInChildren<DraggableItem>();
            if (draggableItem == null || plug.PlugColor != draggableItem.PlugColor)
            {
                completed = false;
            }
        }

        if (completed)
        {
            OnRepairComplete?.Invoke(true);
        }
    }

    private void SwapPlugChildren()
    {
        if (_plugs.Count < 2)
        {
            return;
        }

        List<DraggableItem> draggableItems = new();
        foreach (Plug plug in _plugs)
        {
            DraggableItem child = plug.GetComponentInChildren<DraggableItem>();
            if (child != null)
            {
                draggableItems.Add(child);
            }
        }

        if (draggableItems.Count != _plugs.Count)
        {
            return;
        }

        for (int i = 0; i < draggableItems.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, draggableItems.Count);
            (draggableItems[i], draggableItems[randomIndex]) = (draggableItems[randomIndex], draggableItems[i]);
        }

        for (int i = 0; i < _plugs.Count; i++)
        {
            draggableItems[i].transform.SetParent(_plugs[i].transform);
            draggableItems[i].transform.localPosition = Vector3.zero;
        }
    }
}
