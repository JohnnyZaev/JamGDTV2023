using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorkShops : MonoBehaviour
{
    [SerializeField] private List<GameObject> workshops;
    [SerializeField] private UnityEvent onWorkshopChanged;
    private GameObject _activeWorkshop;
    void Start()
    {
        workshops = new List<GameObject>();

        foreach(Transform child in transform)
        {
            if (child.tag == "WorkShop")
            {
                workshops.Add(child.gameObject);
            }
        }
        _activeWorkshop = workshops[0];
        _activeWorkshop.SetActive(true);
    }

    public void ChangeWorkShop(int index)
    {
        onWorkshopChanged.Invoke();
        _activeWorkshop?.SetActive(false);
        _activeWorkshop = workshops[index];
        _activeWorkshop.SetActive(true);
    }


    // Delete when OnWorkShopChanged Event is Assigned
    public void Test()
    {
        Debug.Log("lol");
    }

    
}
