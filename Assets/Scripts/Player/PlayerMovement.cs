using Input;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float playerSpeed = 5f;

        private Rigidbody _playerRb;
        private Vector3 _playerVelocity;
        private Vector3 _lookAtDirection;

        private void Awake()
        {
            _playerRb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            GetInputs();
            MovePlayer();
        }

        private void GetInputs()
        {
            _playerVelocity.x = InputManager.Instance.HorizontalInput * playerSpeed;
            _playerVelocity.y = _playerRb.velocity.y;
            _playerVelocity.z = InputManager.Instance.VerticalInput * playerSpeed;
        }

        private void MovePlayer()
        {
            _lookAtDirection = _playerVelocity.normalized;
            _lookAtDirection.y = 0;
            _playerRb.transform.LookAt(transform.position + _lookAtDirection);
            _playerRb.velocity = _playerVelocity;
        }
    }
}
