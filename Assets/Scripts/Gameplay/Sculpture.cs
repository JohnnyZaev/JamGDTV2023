using System;
using Attributes;
using Dialogue;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class Sculpture : MonoBehaviour, IPointerClickHandler
    {
        [ShowOnly]
        [SerializeField]
        private int currentStage;
        private TutorialManager _tutorialManager;

        [Serializable]
        private struct SculptureStage
        {
            public GameObject sculptureView;
            public DialogueBase[] dialogues;
        }
        [SerializeField]
        private SculptureStage[] sculptureStages;

        private GameInfo.GameInfo _gameInfo;
        private int _dialogueStage = 0;
        private SparkleManager _sparkleManager;

        private void Awake()
        {
            foreach (var sculptureStage in sculptureStages)
            {
                sculptureStage.sculptureView.SetActive(false);
            }
            currentStage = 0;
            sculptureStages[currentStage].sculptureView.SetActive(true);
            _sparkleManager = FindObjectOfType<SparkleManager>();
            _tutorialManager = FindObjectOfType<TutorialManager>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (currentStage >= sculptureStages.Length - 1) return;

            _tutorialManager.ClickSculpture();
            
            if (_sparkleManager.Sparkles > 0)
            {
                --_sparkleManager.Sparkles;
                sculptureStages[currentStage].sculptureView.SetActive(false);
                ++currentStage;
                sculptureStages[currentStage].sculptureView.SetActive(true);
                _dialogueStage = 0;
            }
            DialogueController.Instance.StartDialogue(sculptureStages[currentStage].dialogues[_dialogueStage]);
            if (_dialogueStage < sculptureStages[currentStage].dialogues.Length - 1)
            {
                ++_dialogueStage;
            }
        }
    }
}
