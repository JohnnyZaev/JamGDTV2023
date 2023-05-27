using Puzzles;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Attributes;

namespace Gameplay
{
    public class SparkleObject : InteractableObject
    {
        [SerializeField] private bool isActive = true;
        [Restrict(typeof(IPuzzle))][SerializeField] private GameObject puzzle;
        public UnityEvent OnSuccess;
        public UnityEvent OnFailure;
        private IPuzzle _puzzle;

        private void Awake()
        {
            _puzzle = puzzle.GetComponent<IPuzzle>();
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!isActive) return;

            _puzzle.OnSuccess = OnSuccess;
            _puzzle.OnFailure = OnFailure;
            _puzzle.Start();
        }

        public void NewIsActive(bool newIsActive)
        {
            isActive = newIsActive;
        }
    }
}
