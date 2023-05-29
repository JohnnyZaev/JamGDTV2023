using Gameplay.Interactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Attributes;

namespace Gameplay
{
    public class SparkleObject : InteractableObject
    {
        [SerializeField] private bool isActive = true;
        [Restrict(typeof(IInteraction))][SerializeField] private GameObject puzzle;
        public UnityEvent OnSuccess;
        public UnityEvent OnFailure;
        private IInteraction _interaction;

        private void Awake()
        {
            _interaction = puzzle.GetComponent<IInteraction>();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!isActive) return;

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
