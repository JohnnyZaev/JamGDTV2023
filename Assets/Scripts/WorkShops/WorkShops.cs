using System;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Workshops
{
    public class WorkShops : MonoBehaviour
    {
        [SerializeField] private Light currentTimeLight;
        [SerializeField] private Light pastLight;
        [SerializeField] private WorkshopInfo[] _workshops;
        [SerializeField] private UnityEvent onWorkshopChanged;
        private WorkshopInfo _activeWorkshop;
        private SparkleManager _sparkleManager;
        
        [Serializable]
        private struct WorkshopInfo
        {
            public GameObject WorkShop;
            public int sparklesLeft;
        }

        private void Awake()
        {
            _sparkleManager = FindObjectOfType<SparkleManager>();
        }

        void Start()
        {
            _activeWorkshop = _workshops[0];
            _activeWorkshop.WorkShop.SetActive(true);
        }

        public void ChangeWorkShop(int index)
        {
            if (_activeWorkshop.WorkShop == _workshops[index].WorkShop) return;
            if (_activeWorkshop.WorkShop == _workshops[0].WorkShop && _sparkleManager.Sparkles == _sparkleManager.maxSparkles) return;
            onWorkshopChanged.Invoke();
            _activeWorkshop.WorkShop.SetActive(false);
            _activeWorkshop = _workshops[index];
            _activeWorkshop.WorkShop.SetActive(true);
            if (_activeWorkshop.sparklesLeft > 0)
            {
                currentTimeLight.gameObject.SetActive(false);
                pastLight.gameObject.SetActive(true);
                _sparkleManager.HasEmission = true;
            }
            else
            {
                currentTimeLight.gameObject.SetActive(true);
                pastLight.gameObject.SetActive(false);
                _sparkleManager.HasEmission = false;
            }
        }

        public void SparkleAdded(int workshopIndex)
        {
            _workshops[workshopIndex].sparklesLeft -= 1;

            if (_workshops[workshopIndex].sparklesLeft == 0)
            {
                currentTimeLight.gameObject.SetActive(true);
                pastLight.gameObject.SetActive(false);
                _sparkleManager.HasEmission = false;
            }
        }
    }
}
