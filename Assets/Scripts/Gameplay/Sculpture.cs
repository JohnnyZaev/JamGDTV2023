using System;
using Attributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class Sculpture : MonoBehaviour, IPointerClickHandler
    {
        [ShowOnly]
        [SerializeField]
        private int currentStage;
    
        [Serializable]
        private struct SculptureStage
        {
            public GameObject sculptureView;
            [Min(0)]
            public int sparklesToChange;
        }
        [SerializeField]
        private SculptureStage[] sculptureStages;

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
            if (currentStage >= sculptureStages.Length - 1) return;
            if (5 >= sculptureStages[currentStage].sparklesToChange) // TODO: Connect with info
            {
                sculptureStages[currentStage].sculptureView.SetActive(false);
                ++currentStage;
                sculptureStages[currentStage].sculptureView.SetActive(true);
            }
        }
    }
}
