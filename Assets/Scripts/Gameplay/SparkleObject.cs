using Puzzles;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Attributes;

namespace Gameplay
{
    public class SparkleObject : InteractableObject
    {
        public UnityEvent OnSuccess;
        public UnityEvent OnFailure;
        [SerializeField] private bool isActive = true;
        [Restrict(typeof(IPuzzle))] [SerializeField] private GameObject puzzle;
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

        public void SetActive(bool isActive)
        {
            this.isActive = isActive;
        }
    }
}
