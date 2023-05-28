using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerAppearance : MonoBehaviour
    {
        [SerializeField] private float maxEmission = 3;
        [SerializeField] private int maxSparkles = 3;
        [SerializeField] private float sparkleSpawnTime = 0.5f;
        [SerializeField] private CollectedSparkle visualCollectedSparklePrefab;
        
        private CollectedSparkle[] _visualCollectedSparkles;
        
        private Material _material;

        private Coroutine _waitForRightPosition;
        private int _visualActiveSparkles;
        private int _sparkles;
        public int Sparkles
        {
            get => _sparkles;
            set
            {
                if (value < 0 || value < _sparkles)
                {
                    _sparkles = 0;
                } else if (value > maxSparkles)
                {
                    _sparkles = maxSparkles;
                }
                else
                {
                    _sparkles = value;
                }
                UpdateVisualSparklesAmount();
                UpdateEmission();
            }
        }

        public void TestAddSparkle() // TODO: Remove test function
        {
            Sparkles += 1;
            Debug.Log("Test ");
        }

        private void Awake()
        {
            _material = GetComponent<Material>();
            _visualCollectedSparkles = new CollectedSparkle[maxSparkles];
            for (int i = 0; i < _visualCollectedSparkles.Length; i++)
            {
                _visualCollectedSparkles[i] = Instantiate(visualCollectedSparklePrefab, transform);
                _visualCollectedSparkles[i].gameObject.SetActive(false);
            }

            _sparkles = 0;
            _visualActiveSparkles = 0;
        }

        private void UpdateVisualSparklesAmount()
        {
            if (_sparkles == 0)
            {
                for (int i = 0; i < _visualActiveSparkles; --_visualActiveSparkles)
                {
                    _visualCollectedSparkles[_visualActiveSparkles - 1].gameObject.SetActive(false);
                }

                _visualActiveSparkles = _sparkles;
            }
            else
            {
                var targetAngleDifference = 360.0f / _sparkles;
                for (int i = 0; i < _sparkles; i++)
                {
                    _visualCollectedSparkles[i].transform.localRotation = _visualCollectedSparkles[0].transform.localRotation * Quaternion.Euler(0f, targetAngleDifference * i, 0f);
                    _visualCollectedSparkles[i].gameObject.SetActive(true);
                }
                _visualActiveSparkles = _sparkles;
            }
        }

        private void UpdateEmission()
        {
            
        }
    }
}
