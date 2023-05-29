using UnityEngine;

namespace Workshops
{
    public class AnimationWorkShopChange : MonoBehaviour
    {
        [SerializeField] private WorkShops _workShops;
        private int _index;
        public void SetIndex(int index)
        {
            _index = index;
        }

        public void ChangeWorkShop()
        {
            _workShops.ChangeWorkShop(_index);
        }
    }
}
