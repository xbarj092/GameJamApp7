using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement1D : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 5f;

    private InputAction _movementInput;
    private Queue<InputFrame> _inputBuffer = new();

    private struct InputFrame
    {
        public Vector2 Input;
        public float Timestamp;
    }

    private void Awake()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        _movementInput = playerInput.actions.FindAction("Move");
    }

    private void Update()
    {
        if (ScreenManager.Instance.ActiveGameScreen == null)
        {
            BufferCurrentInput();
            ProcessBufferedInput();
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private void BufferCurrentInput()
    {
        Vector2 input = _movementInput.ReadValue<Vector2>();
        _inputBuffer.Enqueue(new InputFrame { Input = input, Timestamp = Time.time });
    }

    private void ProcessBufferedInput()
    {
        int bufferCount = _inputBuffer.Count;

        for (int i = 0; i < bufferCount; i++)
        {
            InputFrame frame = _inputBuffer.Peek();

            if (Time.time >= frame.Timestamp + EventManager.Instance.InputDelay)
            {
                _inputBuffer.Dequeue();
                MoveOnLine(frame.Input.x);
            }
        }
    }

    private void MoveOnLine(float input)
    {
        Vector3 direction = (pointB.position - pointA.position).normalized;

        Vector3 movement = input * moveSpeed * Time.deltaTime * direction;

        Vector3 targetPosition = transform.position + movement;

        Vector3 lineDirection = (pointB.position - pointA.position).normalized;
        Vector3 pointToTarget = targetPosition - pointA.position;
        float projection = Vector3.Dot(pointToTarget, lineDirection);
        targetPosition = pointA.position + lineDirection * projection;
        transform.position = new Vector3(targetPosition.x,0,0);
    }
}
