using System;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Workshops
{
    public class WorkShops : MonoBehaviour
    {
        [SerializeField] private UnityEvent onWorkshopChanged;
        private GameObject[] _workshops;
        private GameObject _activeWorkshop;
        private int index = 0;
        private SparkleManager _sparkleManager;

        private void Awake()
        {
            _sparkleManager = FindObjectOfType<SparkleManager>();
        }

        void Start()
        {
            _workshops = new GameObject[gameObject.transform.childCount];
            foreach (Transform child in transform)
            {
                _workshops[index++] = child.gameObject;
            }
            _activeWorkshop = _workshops[0];
            _activeWorkshop.SetActive(true);
        }

        public void ChangeWorkShop(int index)
        {
            if (_activeWorkshop == _workshops[index]) return;
            if (_activeWorkshop == _workshops[0] && _sparkleManager.Sparkles > 0) return;
            onWorkshopChanged.Invoke();
            _activeWorkshop.SetActive(false);
            _activeWorkshop = _workshops[index];
            _activeWorkshop.SetActive(true);
        }
    }
}
