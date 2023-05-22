using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class SparkleObject : InteractableObject
    {
        [SerializeField] private UnityEvent onClick;
    
        public override void OnPointerClick(PointerEventData eventData)
        {
            onClick.Invoke();
            Debug.Log(gameObject.name); // TODO : Delete after tests
        }
    }
}
