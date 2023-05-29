using UnityEngine.Events;

namespace Gameplay.Interactions
{
    public interface IInteraction
    {
        public void Start();
        public UnityEvent OnSuccess { get; set; }
        public UnityEvent OnFailure { get; set; }
    }
}
