using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private GameObject _torusGroundBase;
    [SerializeField] private GameObject _torusWallBase;

    [SerializeField] private GameObject _torusGroundEnd;
    [SerializeField] private GameObject _torusWallEnd;
    [SerializeField] private GameObject _enemyOrb;

    private Vector3 _lastPosition;
    private bool _isPlayerInTrigger = false;
    private bool _increasedLevel = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            _lastPosition = other.transform.position;
            _isPlayerInTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(GlobalConstants.Tags.Player.ToString()) && _isPlayerInTrigger)
        {
            Vector3 playerPosition = other.transform.position;
            Vector3 radialVector = (playerPosition - transform.position).normalized;
            Vector3 tangentVector = new(-radialVector.y, radialVector.x, 0);

            Vector3 velocity = (playerPosition - _lastPosition).normalized;
            float dotProduct = Vector3.Dot(velocity, tangentVector);

            if (dotProduct >= 0)
            {
                Debug.Log("Moving Clockwise");
            }
            else
            {
                Debug.Log("Moving Counterclockwise");
                _increasedLevel = true;
                _isPlayerInTrigger = false;
            }

            _lastPosition = playerPosition;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            if (_increasedLevel)
            {
                GameManager.Instance.IncreaseLevel();
            }
            else
            {
                GameManager.Instance.DecreaseLevel();
            }

            SwitchModels(GameManager.Instance.CurrentLevel == 5);
            _isPlayerInTrigger = false;
            _increasedLevel = false;
        }
    }

    private void SwitchModels(bool end)
    {
        _torusGroundBase.SetActive(!end);
        _torusWallBase.SetActive(!end);
        _torusGroundEnd.SetActive(end);
        _torusWallEnd.SetActive(end);
        _enemyOrb.SetActive(end);
    }
}
