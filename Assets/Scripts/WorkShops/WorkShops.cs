using UnityEngine;
using UnityEngine.Events;

namespace Workshops
{
    public class WorkShops : MonoBehaviour
    {
        [SerializeField] private UnityEvent onWorkshopChanged;
        private WorkShop[] _workshops;
        private WorkShop _activeWorkshop;
        private int index = 0;
        void Start()
        {
            _workshops = new WorkShop[gameObject.transform.childCount];
            foreach (Transform child in transform)
            {
                _workshops[index++] = child.GetComponent<WorkShop>();
            }
            _activeWorkshop = _workshops[0];
            _activeWorkshop.gameObject.SetActive(true);
        }

        public void ChangeWorkShop(int index)
        {
            onWorkshopChanged.Invoke();
            _activeWorkshop?.gameObject.SetActive(false);
            _activeWorkshop = _workshops[index];
            _activeWorkshop.gameObject.SetActive(true);
        }

        // TODO: Delete when OnWorkShopChanged Event is Assigned
        public void Test()
        {
        }
    }
}
