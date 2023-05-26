using Input;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float playerSpeed = 5f;
        [Tooltip("The turning speed in degrees")]
        [SerializeField] private float rotationSpeed = 180.0f;

        private Rigidbody _playerRb;
        private Vector3 _playerVelocity;
        private readonly Quaternion _rotationOffset = Quaternion.Euler(0f, 45, 0f);
        private float _rotationSpeedFixedDelta;
        private Quaternion _targetRotation;

        private void Awake()
        {
            _playerRb = GetComponent<Rigidbody>();
            _rotationSpeedFixedDelta = rotationSpeed * Time.fixedDeltaTime;
        }

        private void FixedUpdate()
        {
            GetInputs();
            MovePlayer();
            RotatePlayer();
        }

        private void GetInputs()
        {
            _playerVelocity.x = InputManager.Instance.HorizontalInput * playerSpeed;
            _playerVelocity.z = InputManager.Instance.VerticalInput * playerSpeed;
            _playerVelocity = _rotationOffset * _playerVelocity;
        }

        private void MovePlayer()
        {
            _playerRb.velocity = _playerVelocity;
        }

        private void RotatePlayer()
        {
            if (_playerVelocity.x != 0 && _playerVelocity.z != 0)
            {
                _targetRotation = Quaternion.LookRotation(_playerRb.velocity, Vector3.up);
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeedFixedDelta);
        }
    }
}
