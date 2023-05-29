using CameraController;
using Dialogue;
using Input;
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
        [SerializeField] private DialogueBase[] dialogue;
        public UnityEvent OnSuccess { get; set; }
        public UnityEvent OnFailure { get; set; }

        private int _dialogueStage = 0;

        private CameraController.CameraController _cameraController;
        private bool isZoomActive;
        private Vector3 _cameraOriginPosition;

        private void Awake()
        {
            _cameraController = FindObjectOfType<CameraController.CameraController>();
            _cameraOriginPosition = _cameraController.transform.position;
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
            _cameraController.Move(sparkle.transform.position);
            _cameraController.Zoom(cameraZoomSettings.zoom, cameraZoomSettings.zoomSpeed);
            _cameraController.Blur(true);
            sparkle.layer = 7;
            uiTimeLine.SetActive(false);
            isZoomActive = true;
            DialogueController.Instance.StartDialogue(dialogue[_dialogueStage]);
        }

        public void ExitZoom()
        {
            _cameraController.Move(_cameraOriginPosition);
            _cameraController.Zoom(cameraOriginPostion.zoom, cameraOriginPostion.zoomSpeed);
            _cameraController.Blur(false);
            sparkle.layer = 6;
            uiTimeLine.SetActive(true);
            isZoomActive = false;
            if (_dialogueStage == 0)
            {
                OnSuccess.Invoke();
            }
            if (_dialogueStage < dialogue.Length - 1)
            {
                ++_dialogueStage;
            }
        }
    }
}
