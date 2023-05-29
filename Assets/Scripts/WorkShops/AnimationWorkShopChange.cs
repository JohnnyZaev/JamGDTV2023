using UnityEngine;

namespace Workshops
{
    public class AnimationWorkShopChange : MonoBehaviour
    {
        [SerializeField] private WorkShops workShops;
        private int _index;
        public void SetIndex(int index)
        {
            _index = index;
        }

        public void ChangeWorkShop()
        {
            workShops.ChangeWorkShop(_index);
        }
    }
}
