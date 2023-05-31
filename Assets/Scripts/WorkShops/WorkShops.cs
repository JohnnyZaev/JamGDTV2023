using System;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Workshops
{
    public class WorkShops : MonoBehaviour
    {
        [SerializeField] private Light light;
        [SerializeField] private WorkshopInfo[] _workshops;
        [SerializeField] private UnityEvent onWorkshopChanged;
        private WorkshopInfo _activeWorkshop;
        private int index = 0;
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
            Debug.Log(index);
            if (_activeWorkshop.WorkShop == _workshops[index].WorkShop) return;
            if (_activeWorkshop.WorkShop == _workshops[0].WorkShop && _sparkleManager.Sparkles == _sparkleManager.maxSparkles) return;
            onWorkshopChanged.Invoke();
            _activeWorkshop.WorkShop.SetActive(false);
            _activeWorkshop = _workshops[index];
            _activeWorkshop.WorkShop.SetActive(true);
            if (_activeWorkshop.sparklesLeft > 0)
            {
                light.gameObject.SetActive(false);
                _sparkleManager.HasEmission = true;
            }
            else
            {
                light.gameObject.SetActive(true);
                _sparkleManager.HasEmission = false;
            }
        }
    }
}
