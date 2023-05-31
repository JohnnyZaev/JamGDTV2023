using UnityEngine;
using Utility;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue")]
    public class DialogueBase : ScriptableObject
    {
        public OptionalValue<float> hasTypewriterEffect;
        public OptionalValue<DialogueBase> hasNextDialogue;
        public Color textColor;
        public Color backgroundColor;
        public bool isBubbleType;
        public bool isPausingGame;
        public bool isEndGameDialogue;
        [TextArea(10, 40)]public string text;
    }
}
