using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
    
        public bool MenuOpenCloseInput { get; private set; }
        
        public bool LeftMouseButtonInput { get; private set; }
        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }

        private PlayerInput _playerInput;
        private InputAction _menuOpenCloseAction;
        private InputAction _playerMovementAction;
        private InputAction _leftMouseButtonAction;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _playerInput = GetComponent<PlayerInput>();
            _menuOpenCloseAction = _playerInput.actions["MenuOpenClose"];
            _playerMovementAction = _playerInput.actions["PlayerMovement"];
            _leftMouseButtonAction = _playerInput.actions["LeftMouseButton"];
        }

        private void Update()
        {
            MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
            LeftMouseButtonInput = _leftMouseButtonAction.WasPressedThisFrame();
            HorizontalInput = _playerMovementAction.ReadValue<Vector2>().x;
            VerticalInput = _playerMovementAction.ReadValue<Vector2>().y;
        }
    }
}
