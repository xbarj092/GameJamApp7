using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            EventManager.Instance.ChooseEvent();
        }
    }
}
