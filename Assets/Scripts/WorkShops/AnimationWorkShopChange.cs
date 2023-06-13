using UnityEngine;

namespace Workshops
{
    public class AnimationWorkShopChange : MonoBehaviour
    {
        [SerializeField] private WorkShops workShops;
        [SerializeField] private Animator animator;
        private int _index;
        public void SetIndex(int index)
        {
            transform.gameObject.SetActive(true);
            animator.SetTrigger("Blackout");
            _index = index;
        }

        public void ChangeWorkShop()
        {
            workShops.ChangeWorkShop(_index);
        }

        public void TurnOffPanel()
        {
            transform.gameObject.SetActive(false);
        }
    }
}
