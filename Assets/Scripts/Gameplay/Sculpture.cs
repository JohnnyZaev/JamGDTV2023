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
    
        [Serializable]
        private struct SculptureStage
        {
            public GameObject sculptureView;
            [Min(0)]
            public int sparklesToChange;
            public DialogueBase[] dialogues;
        }
        [SerializeField]
        private SculptureStage[] sculptureStages;

        private GameInfo.GameInfo _gameInfo;
        private int _dialogueStage = 0;
        private SparkleManager _sparkleManager;
        private int _collectedSparkles = 0;

        private void Awake()
        {
            foreach (var sculptureStage in sculptureStages)
            {
                sculptureStage.sculptureView.SetActive(false);
            }
            currentStage = 0;
            sculptureStages[currentStage].sculptureView.SetActive(true);
            _sparkleManager = FindObjectOfType<SparkleManager>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (currentStage >= sculptureStages.Length - 1) return;
            _collectedSparkles = _sparkleManager.Sparkles;
            _sparkleManager.Sparkles = 0;
            
            int newStage = currentStage;
            while (_collectedSparkles >= sculptureStages[newStage].sparklesToChange)
            {
                _collectedSparkles -= sculptureStages[newStage].sparklesToChange;
                ++newStage;
            }

            if (newStage != currentStage)
            {
                sculptureStages[currentStage].sculptureView.SetActive(false);
                sculptureStages[newStage].sculptureView.SetActive(true);
                _dialogueStage = 0;
                currentStage = newStage;
            }

            if (_dialogueStage > sculptureStages[currentStage].dialogues.Length - 1)
            {
                return;
            }
            DialogueController.Instance.StartDialogue(sculptureStages[currentStage].dialogues[_dialogueStage]);
            if (_dialogueStage < sculptureStages[currentStage].dialogues.Length - 1)
            {
                ++_dialogueStage;
            }
        }
    }
}
