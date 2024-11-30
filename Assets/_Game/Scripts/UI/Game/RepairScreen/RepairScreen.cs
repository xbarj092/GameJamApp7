using UnityEngine;

public class RepairScreen : GameScreen
{
    [SerializeField] private ChooseRepairSection _chooseRepairSection;
    [SerializeField] private RepairSection _repairSection;

    private GameEvent _chosenGameEvent;

    private void OnEnable()
    {
        _chooseRepairSection.OnRepairChosen += OnRepairChosen;
        _repairSection.OnRepairComplete += OnRepairComplete;
    }

    private void OnDisable()
    {
        _chooseRepairSection.OnRepairChosen -= OnRepairChosen;
        _repairSection.OnRepairComplete -= OnRepairComplete;
    }

    private void OnRepairChosen(GameEvent gameEvent)
    {
        _chosenGameEvent = gameEvent;
        _chooseRepairSection.gameObject.SetActive(false);
        _repairSection.gameObject.SetActive(true);
        _repairSection.StartRepairing();
    }

    private void OnRepairComplete(bool success)
    {
        GameManager.Instance.OnRepairTimerResetInvoke();

        if (success)
        {
            EventManager.Instance.StopPermanentEvent(_chosenGameEvent);
        }

        CloseScreen();
    }
}
