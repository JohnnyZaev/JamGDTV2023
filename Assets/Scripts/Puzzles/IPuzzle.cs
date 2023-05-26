using UnityEngine.Events;

namespace Puzzles
{
    public interface IPuzzle
    {
        public void Start();
        public UnityEvent OnSuccess { get; set; }
        public UnityEvent OnFailure { get; set; }
    }
}
