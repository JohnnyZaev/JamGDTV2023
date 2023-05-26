using UnityEngine;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueBase starterDialogue;

        private DialogueController _dialogueController;

        private void Awake()
        {
            _dialogueController = FindObjectOfType<DialogueController>();
        }

        public void TriggerDialogue()
        {
            _dialogueController.StartDialogue(starterDialogue);
        }
    }
}
