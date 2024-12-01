using UnityEngine;

public class TorusShadeRotator : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the inspector.
    void Update()
    {
        // Get the player's position relative to the world origin
        Vector3 playerPosition = player.position;

        // Calculate the opposite position
        Vector3 oppositeDirection = playerPosition.normalized;

        // Determine the position for the blocker shape
        Quaternion targetRotation = Quaternion.LookRotation(oppositeDirection, Vector3.up);
        targetRotation = Quaternion.Euler(-90, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
        transform.rotation = targetRotation;
    }
}
