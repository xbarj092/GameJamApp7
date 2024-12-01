using UnityEngine;

public class NextLevel1D : MonoBehaviour
{
    [SerializeField] private Transform _start;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            other.transform.position = _start.position;
            GameManager.Instance.IncreaseLevel();
        }
    }
}
