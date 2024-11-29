using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private InputAction _movementInput;
    //private Vector2 _turnDirection;

    [SerializeField] float speed = 10f;
    
    void Start()
    {
        // Mouse
        //Cursor.lockState = CursorLockMode.Locked;


        // Movement
        PlayerInput playerInput = GetComponent<PlayerInput>();
        _movementInput = playerInput.actions.FindAction("Move");
    }

    private void Update()
    {
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 direction = _movementInput.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * Time.deltaTime * speed;
    }
}
