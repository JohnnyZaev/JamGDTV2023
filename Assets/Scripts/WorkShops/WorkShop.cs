using UnityEngine;
using UnityEngine.Events;

namespace Workshops
{
    public class WorkShop : MonoBehaviour
    {
        [SerializeField] private int maxSparkles;
        [SerializeField] private UnityEvent onSparklesCollected;
        private int _currentSparkles;

        public int CurrentSparkles
        {
            get => _currentSparkles;
            set
            {
                _currentSparkles += 1;
            }
        }

        public void AddSparkles()
        {
            if (CurrentSparkles < maxSparkles)
            {
                CurrentSparkles++;

                if (CurrentSparkles == maxSparkles)
                {
                    onSparklesCollected.Invoke();
                }
            }
        }
    }
}
