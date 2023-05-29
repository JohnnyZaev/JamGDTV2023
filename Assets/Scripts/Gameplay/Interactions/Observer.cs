using CameraController;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Interactions
{
    public class Observer : MonoBehaviour, IInteraction
    {
        [SerializeField] private GameObject sparkle;
        [SerializeField] CamerSettings  cameraSettings;
        public UnityEvent OnSuccess { get; set; }
        public UnityEvent OnFailure { get; set; }

        private CameraController.CameraController _cameraController;

        private void Awake()
        {
            _cameraController = FindObjectOfType<CameraController.CameraController>();
        }
        void IInteraction.Start()
        {
            _cameraController.Move(sparkle.transform.position);
            _cameraController.Zoom(cameraSettings.zoom, cameraSettings.zoomSpeed);
            _cameraController.Blur(true);
            sparkle.layer = 7;
        }
    }
}
