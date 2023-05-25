using System;
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
        [SerializeField] private GameObject fullScreenTextObject;
        [SerializeField] private TMP_Text fullScreenTextField;
        [SerializeField] private GameObject bubbleScreenTextObject;
        [SerializeField] private TMP_Text bubbleScreenTextField;

        private GameObject _player;
        private PauseController _pauseController;
        private bool _isClicked;
        private Image _fullScreenTextImage;

        private bool _isDialogueRunning;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _pauseController = FindObjectOfType<PauseController>();
            _fullScreenTextImage = fullScreenTextObject.GetComponent<Image>();
        }

        private void Update()
        {
            if (!_isDialogueRunning)
                return;
            if (InputManager.Instance.LeftMouseButtonInput)
            {
                _isClicked = true;
            }
        }

        public void StartDialogue(DialogueBase dialogue)
        {
            if (dialogue.isBubbleType)
            {
                StartBubbleDialogue(dialogue);
            }
            else
            {
                StartCoroutine(StartFullScreenDialogue(dialogue));
            }
        }

        private IEnumerator StartFullScreenDialogue(DialogueBase dialogue)
        {
            if (_isDialogueRunning)
                yield break;
            _isClicked = false;
            _isDialogueRunning = true;
            _fullScreenTextImage.color = dialogue.backgroundColor;
            fullScreenTextField.color = dialogue.textColor;
            fullScreenTextField.text = "";
            fullScreenTextObject.SetActive(true);
            if (dialogue.hasTypewriterEffect.hasValue)
            {
                var waitTime = new WaitForSecondsRealtime(dialogue.hasTypewriterEffect);
                foreach (var c in dialogue.text.ToCharArray())
                {
                    fullScreenTextField.text += c;
                    if (_isClicked)
                    {
                        fullScreenTextField.text = dialogue.text;
                        _isClicked = false;
                        yield return new WaitForSecondsRealtime(0.5f);
                        break;
                    }
                    yield return waitTime;
                }
            }
            else
            {
                fullScreenTextField.text = dialogue.text;
                yield return new WaitForSecondsRealtime(0.5f);
            }

            yield return new WaitUntil(() => InputManager.Instance.LeftMouseButtonInput);

            fullScreenTextObject.SetActive(false);
            _isDialogueRunning = false;
            if (dialogue.hasNextDialogue.hasValue)
            {
                StartDialogue(dialogue.hasNextDialogue);
            }
        }

        private void StartBubbleDialogue(DialogueBase dialogue)
        {
            throw new NotImplementedException();
        }
    }
}