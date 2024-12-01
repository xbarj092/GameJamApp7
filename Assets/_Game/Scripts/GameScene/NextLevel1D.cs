using UnityEngine;

public class NextLevel1D : MonoBehaviour
{
    [SerializeField] private Transform _start;
    [SerializeField] private bool _next;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            other.transform.position = _start.position;
            if (_next)
            {
                GameManager.Instance.IncreaseLevel();
            }
            else
            {
                GameManager.Instance.DecreaseLevel();
            }
        }
    }
}
