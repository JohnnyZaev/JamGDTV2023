using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorkShops : MonoBehaviour
{
    [SerializeField] private UnityEvent onWorkshopChanged;
    private GameObject[] _workshops;
    private GameObject _activeWorkshop;
    private int index = 0;
    void Start()
    {
        _workshops = new GameObject[gameObject.transform.childCount];
        foreach(Transform child in transform)
        {
            _workshops[index++] = child.gameObject;
        }
        _activeWorkshop = _workshops[0];
        _activeWorkshop.SetActive(true);
        List<int> list = new List<int>();
    }

    public void ChangeWorkShop(int index)
    {
        onWorkshopChanged.Invoke();
        _activeWorkshop?.SetActive(false);
        _activeWorkshop = _workshops[index];
        _activeWorkshop.SetActive(true);
    }


    // Delete when OnWorkShopChanged Event is Assigned
    public void Test()
    {
    }

    
}
