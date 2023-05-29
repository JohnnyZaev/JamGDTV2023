using CameraController;
using Dialogue;
using Input;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Interactions
{
    public class Observer : MonoBehaviour, IInteraction
    {
        [SerializeField] private GameObject sparkle;
        [SerializeField] CamerSettings  cameraZoomSettings;
        [SerializeField] CamerSettings cameraOriginPostion;
        [SerializeField] GameObject uiTimeLine;
        public UnityEvent OnSuccess { get; set; }
        public UnityEvent OnFailure { get; set; }

        private CameraController.CameraController _cameraController;
        private bool isZoomActive;
        private Vector3 _cameraOriginPosition;

        private void Awake()
        {
            _cameraController = FindObjectOfType<CameraController.CameraController>();
        }

        private void Update()
        {
           if (InputManager.Instance.LeftMouseButtonInput && isZoomActive && !DialogueController.Instance.IsDialogueRunning)
            {
                ExitZoom();
            }
        }
        void IInteraction.Start()
        {
            OnSuccess.Invoke();
            _cameraOriginPosition = _cameraController.transform.position;
            _cameraController.Move(sparkle.transform.position);
            _cameraController.Zoom(cameraZoomSettings.zoom, cameraZoomSettings.zoomSpeed);
            _cameraController.Blur(true);
            sparkle.layer = 7;
            uiTimeLine.SetActive(false);
            isZoomActive = true;
        }

        public void ExitZoom()
        {
            _cameraController.Move(_cameraOriginPosition);
            _cameraController.Zoom(cameraOriginPostion.zoom, cameraOriginPostion.zoomSpeed);
            _cameraController.Blur(false);
            sparkle.layer = 6;
            uiTimeLine.SetActive(true);
            isZoomActive = false;
        }
    }
}
