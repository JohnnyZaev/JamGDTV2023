using Gameplay.Interactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Attributes;
using Unity.VisualScripting;

namespace Gameplay
{
    public class SparkleObject : InteractableObject
    {
        [SerializeField] private bool isActive = true;
        [Restrict(typeof(IInteraction))][SerializeField] private GameObject puzzle;
        public UnityEvent OnSuccess;
        public UnityEvent OnFailure;
        private TutorialManager _tutorialManager;
        private IInteraction _interaction;

        private void Awake()
        {
            _interaction = puzzle.GetComponent<IInteraction>();
            _tutorialManager = FindObjectOfType<TutorialManager>();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!isActive) return;

            _tutorialManager.ClickItem();
            _interaction.OnSuccess = OnSuccess;
            _interaction.OnFailure = OnFailure;
            _interaction.Start();
        }

        public void IsActive(bool newIsActive)
        {
            isActive = newIsActive;
        }
    }
}
