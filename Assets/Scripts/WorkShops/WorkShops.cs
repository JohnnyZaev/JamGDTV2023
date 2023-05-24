using UnityEngine;
using UnityEngine.Events;

public class WorkShops : MonoBehaviour
{
    [SerializeField] private UnityEvent onWorkshopChanged;
    [SerializeField] private WorkShop[] workshops;
    private WorkShop _activeWorkshop;
    private int index = 0;
    void Start()
    {
        workshops = new WorkShop[gameObject.transform.childCount];
        foreach(Transform child in transform)
        {
            workshops[index++] = child.GetComponent<WorkShop>();
        }
        _activeWorkshop = workshops[0];
        _activeWorkshop.gameObject.SetActive(true);
    }

    public void ChangeWorkShop(int index)
    {
        onWorkshopChanged.Invoke();
        _activeWorkshop?.gameObject.SetActive(false);
        _activeWorkshop = workshops[index];
        _activeWorkshop.gameObject.SetActive(true);
    }


    // Delete when OnWorkShopChanged Event is Assigned
    public void Test()
    {
    }

    
}
