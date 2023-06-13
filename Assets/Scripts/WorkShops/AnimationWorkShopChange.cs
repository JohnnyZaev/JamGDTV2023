using UnityEngine;

namespace Workshops
{
    public class AnimationWorkShopChange : MonoBehaviour
    {
        [SerializeField] private WorkShops workShops;
        private int _index;
        private bool _isSecondSceneActivated = false;
        public void SetIndex(int index)
        {
            transform.gameObject.SetActive(true);
            if (index != 1)
                _index = index;
            else
            {
                if (_isSecondSceneActivated)
                    _index = index;
            }
        }

        public void ChangeWorkShop()
        {
            workShops.ChangeWorkShop(_index);
        }

        public void TurnOffPanel()
        {
            transform.gameObject.SetActive(false);
        }

        public void ActivateSecondScene()
        {
            _isSecondSceneActivated = true;
        }
    }
}
