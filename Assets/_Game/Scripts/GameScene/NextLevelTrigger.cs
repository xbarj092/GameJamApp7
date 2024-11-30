using UnityEngine;

public class NextLevel : MonoBehaviour
{
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

            _isPlayerInTrigger = false;
            _increasedLevel = false;
        }
    }
}
