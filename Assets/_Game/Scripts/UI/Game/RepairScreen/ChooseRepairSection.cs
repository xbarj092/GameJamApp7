using System;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRepairSection : MonoBehaviour
{
    [SerializeField] private RepairSlot _repairSlotPrefab;
    [SerializeField] private Transform _spawnTransform;

    private List<RepairSlot> _repairSlots = new();

    public event Action<GameEvent> OnRepairChosen;

    public void Start()
    {
        InitSlots();
    }

    private void OnDisable()
    {
        foreach (RepairSlot slot in _repairSlots)
        {
            slot.Button.onClick.RemoveAllListeners();
        }
    }

    private void InitSlots()
    {
        foreach (GameEvent gameEvent in EventManager.Instance.ActivePermanentEvents)
        {
            RepairSlot slot = Instantiate(_repairSlotPrefab, _spawnTransform);
            slot.Init(gameEvent);
            _repairSlots.Add(slot);
            slot.Button.onClick.AddListener(() => OnRepairChosenInvoke(gameEvent));
        }
    }

    public void OnRepairChosenInvoke(GameEvent gameEvent)
    {
        OnRepairChosen?.Invoke(gameEvent);
    }
}
