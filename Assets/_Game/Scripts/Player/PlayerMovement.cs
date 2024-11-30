using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private PlayerMovement _playerMovement;
    private InputAction _movementInput;

    [SerializeField] float speed = 10f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Movement
        PlayerInput playerInput = GetComponent<PlayerInput>();
        _movementInput = playerInput.actions.FindAction("Move");
    }

    void FixedUpdate()
    {
        RotatePlayerWithCamera();
        MovePlayer();
    }

    private void RotatePlayerWithCamera()
    {
        // Get the camera's current rotation
        Transform cameraTransform = virtualCamera.transform;
        Vector3 cameraForward = cameraTransform.forward;

        // Ignore the vertical tilt (only use horizontal rotation)
        cameraForward.y = 0;
        cameraForward.Normalize();

        // Rotate the player to match the camera's forward direction
        if (cameraForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(cameraForward);
        }
    }

    void MovePlayer()
    {
        // Read movement input
        Vector2 direction = _movementInput.ReadValue<Vector2>();

        // Convert local movement to world space based on the player's rotation
        Vector3 moveDirection = transform.TransformDirection(new Vector3(direction.x, 0, direction.y));

        // Move the player
        transform.position += moveDirection * Time.deltaTime * speed;
    }
}
