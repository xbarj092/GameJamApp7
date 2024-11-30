using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private Vector3 lastPosition;
    private bool isPlayerInTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            lastPosition = other.transform.position;
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(GlobalConstants.Tags.Player.ToString()) && isPlayerInTrigger)
        {
            Vector3 playerPosition = other.transform.position;
            Vector3 radialVector = (playerPosition - transform.position).normalized;
            Vector3 tangentVector = new(-radialVector.y, radialVector.x, 0);

            Vector3 velocity = (playerPosition - lastPosition).normalized;
            float dotProduct = Vector3.Dot(velocity, tangentVector);

            if (dotProduct >= 0)
            {
                Debug.Log("Moving Clockwise");
            }
            else
            {
                Debug.Log("Moving Counterclockwise");
                EventManager.Instance.ChooseEvent();
                isPlayerInTrigger = false;
            }

            lastPosition = playerPosition;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            isPlayerInTrigger = false;
        }
    }
}