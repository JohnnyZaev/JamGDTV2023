using System.Collections;
using Input;
using MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

        public DialogueBase starterDialogue;

        private GameObject _player;
        private PauseController _pauseController;
        private bool _isClicked;
        private Image _fullScreenTextImage;
        private Image _bubbleScreenTextImage;
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
            _bubbleScreenTextImage = bubbleScreenTextObject.GetComponent<Image>();
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
            StartCoroutine(Talk(dialogue));
        }

        public void StartFirstDialogue(DialogueBase dialogue, UnityEvent startGame)
        {
            StartCoroutine(TalkFirstDialogue(dialogue, startGame));
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
                textImage = _bubbleScreenTextImage;
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

        private IEnumerator TalkFirstDialogue(DialogueBase dialogue, UnityEvent startGame)
        {
            if (IsDialogueRunning)
                yield break;
            Image textImage;
            TMP_Text textField;
            GameObject textObject;
            if (dialogue.isBubbleType)
            {
                textImage = _bubbleScreenTextImage;
                textField = bubbleScreenTextField;
                textObject = bubbleScreenTextObject;
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
                TalkFirstDialogue(dialogue.hasNextDialogue, startGame);
            }
            startGame.Invoke();
        }
    }
}