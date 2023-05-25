using System;
using System.Collections.Generic;
using Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Sculpture : MonoBehaviour, IPointerClickHandler
{
    [ShowOnly]
    [SerializeField]
    private int currentStage;
    
    [Serializable]
    public struct SculptureStage
    {
        public GameObject sculptureView;
        [Min(0)]
        public int sparklesToChange;
    }
    [SerializeField] private List<SculptureStage> sculptureStages;

    private void Awake()
    {
        foreach (var sculptureStage in sculptureStages)
        {
            sculptureStage.sculptureView.SetActive(false);
        }
        currentStage = 0;
        sculptureStages[currentStage].sculptureView.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentStage >= sculptureStages.Count - 1) return;
        if (5 >= sculptureStages[currentStage].sparklesToChange) // TODO: Connect with info
        {
            sculptureStages[currentStage].sculptureView.SetActive(false);
            ++currentStage;
            sculptureStages[currentStage].sculptureView.SetActive(true);
        }
    }
}
