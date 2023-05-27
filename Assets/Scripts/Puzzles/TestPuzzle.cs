using UnityEngine;
using UnityEngine.Events;

namespace Puzzles
{
    public class TestPuzzle : MonoBehaviour, IPuzzle
    {
        public UnityEvent OnSuccess { get; set; }
        public UnityEvent OnFailure { get; set; }

        void IPuzzle.Start()
        {
            OnSuccess.Invoke();
        }
    }
}
