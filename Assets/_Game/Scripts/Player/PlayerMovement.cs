using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 10f;

    [Space(5)]
    [SerializeField] private NoiseSettings _standingNoise;
    [SerializeField] private NoiseSettings _movingNoise;
    private CinemachineBasicMultiChannelPerlin _noise;

    private InputAction _movementInput;
    private Queue<InputFrame> _inputBuffer = new();

    private struct InputFrame
    {
        public Vector2 Input;
        public float Timestamp;
    }

    private void Awake()
    {
        _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Cursor.lockState = CursorLockMode.Confined;

        PlayerInput playerInput = GetComponent<PlayerInput>();
        _movementInput = playerInput.actions.FindAction("Move");
    }

    private void Update()
    {
        if (ScreenManager.Instance.ActiveGameScreen == null && TextManager.Instance.CurrentText == null)
        {
            RotatePlayerWithCamera();
            BufferCurrentInput();
            ProcessBufferedInput();
        }
        else
        {
            if (_noise != null)
            {
                _noise.m_NoiseProfile = _standingNoise;
            }

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
                MovePlayer(frame.Input);
            }
        }
    }

    private void RotatePlayerWithCamera()
    {
        Transform cameraTransform = _virtualCamera.transform;
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        if (cameraForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(cameraForward);
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            if (_noise != null)
            {
                _noise.m_NoiseProfile = _standingNoise;
            }

            return;
        }

        if (_noise != null)
        {
            _noise.m_NoiseProfile = _movingNoise;
        }

        Vector3 moveDirection = transform.TransformDirection(new Vector3(direction.x, 0, direction.y)).normalized;
        Vector3 targetPosition = _rigidbody.position + _speed * Time.deltaTime * moveDirection;

        _rigidbody.MovePosition(targetPosition);
    }
}
