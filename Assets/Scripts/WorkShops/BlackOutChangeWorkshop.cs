using UnityEngine;

namespace WorkShops
{
    public class BlackOutChangeWorkshop : MonoBehaviour
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
