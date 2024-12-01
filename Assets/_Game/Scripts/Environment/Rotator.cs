using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    [Header("Rotation Speed Settings")]
    [Tooltip("The minimum speed of rotation in degrees per second.")]
    public float minRotationSpeed = 5f;

    [Tooltip("The maximum speed of rotation in degrees per second.")]
    public float maxRotationSpeed = 30f;

    [Header("Random Axis Update")]
    [Tooltip("Should the rotation axis change periodically?")]
    public bool randomizeAxisOverTime = false;

    [Tooltip("How often (in seconds) to change the rotation axis.")]
    public float axisChangeInterval = 5f;

    private Vector3 currentRotationAxis;
    private float currentRotationSpeed;
    private float timeSinceLastAxisChange = 0f;

    void Start()
    {
        SetRandomRotation();
    }

    void Update()
    {
        // Rotate the object continuously
        transform.Rotate(currentRotationAxis * currentRotationSpeed * Time.deltaTime, Space.Self);

        // Optionally randomize the rotation axis over time
        if (randomizeAxisOverTime)
        {
            timeSinceLastAxisChange += Time.deltaTime;
            if (timeSinceLastAxisChange >= axisChangeInterval)
            {
                SetRandomRotation();
                timeSinceLastAxisChange = 0f;
            }
        }
    }

    private void SetRandomRotation()
    {
        // Generate a random rotation axis
        currentRotationAxis = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        // Generate a random rotation speed within the specified range
        currentRotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }
}
