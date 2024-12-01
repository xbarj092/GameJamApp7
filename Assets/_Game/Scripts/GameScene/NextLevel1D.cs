using UnityEngine;

public class NextLevel1D : MonoBehaviour
{
    [SerializeField] private GameObject _lineNormal;
    [SerializeField] private GameObject _lineEnd;

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

            // SwitchModels(GameManager.Instance.CurrentLevel == 5);
        }
    }

    private void SwitchModels(bool end)
    {
        _lineNormal.SetActive(!end);
        _lineEnd.SetActive(end);
    }
}
