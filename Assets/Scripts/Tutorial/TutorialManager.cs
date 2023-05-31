using Dialogue;
using Input;
using Player;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> tutorialText;
    [SerializeField] private TextMeshProUGUI tutorialArea;
    [SerializeField] private float changeTutorialPositionAmount;
    private SparkleManager _sparkleManager;
    private int  _tutorialIndex = 0;
    private bool _isTimelineClicked = false;
    private bool _isItemClicked = false;
    private bool _isSculptureClicked = false;

    private void Awake()
    {
        _sparkleManager = FindObjectOfType<SparkleManager>();
        tutorialArea.text = tutorialText[_tutorialIndex].text;
    }

    private void Update()
    {
        ChangeHint();
    }

    public void ChangeHint()
    {
        if (_tutorialIndex == 0)
        {
            if (InputManager.Instance.LeftMouseButtonInput && !DialogueController.Instance.starterDialogue.hasNextDialogue.hasValue)
            {
                _tutorialIndex++;
                tutorialArea.text = tutorialText[_tutorialIndex].text;
            }
        }
        else if (_tutorialIndex == 1)
        {
            if (_isTimelineClicked)
            {
                _tutorialIndex++;
                tutorialArea.text = tutorialText[_tutorialIndex].text;
            }
        }
        else if (_tutorialIndex == 2)
        {
            if (_isItemClicked)
            {
                _tutorialIndex++;
                tutorialArea.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, changeTutorialPositionAmount);
                tutorialArea.text = tutorialText[_tutorialIndex].text;
            }
        }
        else if (_tutorialIndex == 3)
        {
            if (InputManager.Instance.LeftMouseButtonInput)
            {
                _tutorialIndex++;
                tutorialArea.text = tutorialText[_tutorialIndex].text;
                _isItemClicked = false;
            }
        }
        else if (_tutorialIndex == 4)
        {
            if (_isItemClicked)
            {
                tutorialArea.gameObject.SetActive(false);
            }
            if (_sparkleManager.Sparkles == 3)
            {
                tutorialArea.gameObject.SetActive(true);
                tutorialArea.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, changeTutorialPositionAmount);
                _tutorialIndex++;
                tutorialArea.text = tutorialText[_tutorialIndex].text;
            }
        }
        else if (_tutorialIndex == 5)
        {
            if (_isSculptureClicked)
            {
                _tutorialIndex++;
                tutorialArea.text = tutorialText[_tutorialIndex].text;
            }
        }
        else if (_tutorialIndex == 6)
        {
            if (InputManager.Instance.LeftMouseButtonInput)
            {
                tutorialArea.gameObject.SetActive(false);
            }
        }

    }

    public void ClickTimeline()
    {
        _isTimelineClicked = true;
    }

    public void ClickItem()
    {
        _isItemClicked = true;
    }

    public void ClickSculpture()
    {
        _isSculptureClicked = true;
    }
}
