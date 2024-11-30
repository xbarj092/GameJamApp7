using UnityEngine;
using UnityEngine.UI;

public class RepairSlot : MonoBehaviour
{
    [SerializeField] private Image _eventTypeImage;

    public Button Button;

    public void Init(GameEvent gameEvent)
    {
        _eventTypeImage.sprite = gameEvent.Sprite;
    }
}
