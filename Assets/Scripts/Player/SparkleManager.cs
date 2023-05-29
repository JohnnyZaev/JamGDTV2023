using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class SparkleManager : MonoBehaviour
    {
        [SerializeField] private float maxEmission = 3;
        public int maxSparkles = 3;
        [SerializeField] private CollectedSparkle visualCollectedSparklePrefab;
        [SerializeField] private GameObject player;
        
        private CollectedSparkle[] _visualCollectedSparkles;
        
        private Material _material;
        private Color _baseColor;

        private Coroutine _waitForRightPosition;
        private int _visualActiveSparkles;
        [SerializeField] private int _sparkles;
        private static readonly int EmissiveColorName = Shader.PropertyToID("_EmissionColor");

        private Workshops.WorkShops _workshops;

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
                if (_sparkles == maxSparkles)
                {
                    _workshops.ChangeWorkShop(0);
                }
            }
        }

        public void AddSparkle()
        {
            Sparkles += 1;
        }

        private void Awake()
        {
            _material = player.GetComponent<Renderer>().material;
            _baseColor = _material.GetColor(EmissiveColorName);
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
            var brightness = Mathf.Pow(2,((10 + maxEmission) / maxSparkles * _sparkles));
            _material.SetColor(EmissiveColorName,new Color(
                _baseColor.r * brightness,
                _baseColor.g * brightness,
                _baseColor.b * brightness,
                _baseColor.a));
        }
    }
}
