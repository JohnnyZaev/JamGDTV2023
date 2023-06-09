using System.Collections;
using Input;
using MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        public static DialogueController Instance;

        [SerializeField] private GameObject fullScreenTextObject;
        [SerializeField] private TMP_Text fullScreenTextField;
        [SerializeField] private GameObject bubbleScreenTextObject;
        [SerializeField] private TMP_Text bubbleScreenTextField;
        [SerializeField] private RectTransform bubbleUIPosition;
        [SerializeField] private float maxXPositionForBubble;
        [SerializeField] private float waitTimeAfterDialogue = 0.5f;
        [SerializeField] private Vector3 bubblePosOffset;

        [HideInInspector] public Image bubbleScreenTextImage;
        public DialogueBase starterDialogue;

        private GameObject _player;
        private PauseController _pauseController;
        private bool _isClicked;
        private Image _fullScreenTextImage;
        private Camera _mainCamera;

        private WaitForSecondsRealtime _afterDialogueWait;
        private WaitUntil _waitUntilClick;

        public bool IsDialogueRunning { get; private set; }

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

            _player = GameObject.FindGameObjectWithTag("Player");
            _pauseController = FindObjectOfType<PauseController>();
            _fullScreenTextImage = fullScreenTextObject.GetComponent<Image>();
            bubbleScreenTextImage = bubbleScreenTextObject.GetComponent<Image>();
            _mainCamera = Camera.main;
            _afterDialogueWait = new WaitForSecondsRealtime(waitTimeAfterDialogue);
            _waitUntilClick = new WaitUntil(() => InputManager.Instance.LeftMouseButtonInput);
        }

        private void Update()
        {
            if (!IsDialogueRunning)
                return;
            bubbleScreenTextObject.transform.rotation = _mainCamera.transform.rotation;
            bubbleUIPosition.position = _player.transform.position + bubblePosOffset;
            if (InputManager.Instance.LeftMouseButtonInput)
            {
                _isClicked = true;
            }
        }

        public void StartDialogue(DialogueBase dialogue)
        {
            starterDialogue = dialogue;
            StartCoroutine(Talk(dialogue));
        }

        private IEnumerator Talk(DialogueBase dialogue)
        {
            if (IsDialogueRunning)
                yield break;
            Image textImage;
            TMP_Text textField;
            GameObject textObject;
            if (dialogue.isBubbleType)
            {
                textImage = bubbleScreenTextImage;
                textField = bubbleScreenTextField;
                textObject = bubbleScreenTextObject;
                textObject.SetActive(true);
            }
            else
            {
                textImage = _fullScreenTextImage;
                textField = fullScreenTextField;
                textObject = fullScreenTextObject;
            }
            _isClicked = false;
            IsDialogueRunning = true;
            textImage.color = dialogue.backgroundColor;
            textField.color = dialogue.textColor;
            textField.text = "";
            textObject.SetActive(true);
            if (dialogue.hasTypewriterEffect.hasValue)
            {
                var waitTime = new WaitForSecondsRealtime(dialogue.hasTypewriterEffect);
                foreach (var c in dialogue.text.ToCharArray())
                {
                    textField.text += c;
                    if (_isClicked)
                    {
                        textField.text = dialogue.text;
                        _isClicked = false;
                        yield return _afterDialogueWait;
                        break;
                    }
                    yield return waitTime;
                }
            }
            else
            {
                textField.text = dialogue.text;
                yield return _afterDialogueWait;
            }

            yield return _waitUntilClick;

            _pauseController.BackToGame();
            textObject.SetActive(false);
            IsDialogueRunning = false;
            if (dialogue.hasNextDialogue.hasValue)
            {
                StartDialogue(dialogue.hasNextDialogue);
            }
        }
    }
}