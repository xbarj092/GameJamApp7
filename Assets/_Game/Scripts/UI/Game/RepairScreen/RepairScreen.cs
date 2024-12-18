using UnityEngine;

public class RepairScreen : GameScreen
{
    [SerializeField] private ChooseRepairSection _chooseRepairSection;
    [SerializeField] private RepairSection _repairSection;

    private GameEvent _chosenGameEvent;

    private void Start()
    {
        if (!TextManager.Instance.HasPlayedTutorial(StringStorageType.Repairing))
        {
            TextManager.Instance.ShowText(StringStorageType.Repairing, true);
        }
    }

    private void OnEnable()
    {
        TextManager.Instance.DestroyOldText();

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
