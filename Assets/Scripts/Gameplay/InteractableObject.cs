using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public abstract class InteractableObject : MonoBehaviour, IPointerClickHandler
    {
        public abstract void OnPointerClick(PointerEventData eventData);
    }
}
