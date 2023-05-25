using UnityEditor;
using UnityEngine;
using Utility;

namespace Editor.Utility
{
	[CustomPropertyDrawer(typeof(IOptionalValue), true)]
	public class OptionalValueDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var isEnabled = property.FindPropertyRelative("hasValue");
			if (!isEnabled.boolValue) return EditorGUI.GetPropertyHeight(property, false);
			var value = property.FindPropertyRelative("value");
			return EditorGUI.GetPropertyHeight(value, true) + (value.hasVisibleChildren ? 0f : EditorGUI.GetPropertyHeight(property, false));
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			property.isExpanded = true;

			var isEnabled = property.FindPropertyRelative("hasValue");
			position.height = EditorGUI.GetPropertyHeight(property, label, false);

			if (EditorGUI.ToggleLeft(position, label, isEnabled.boolValue))
			{
				if (!isEnabled.boolValue)
				{
					isEnabled.boolValue = true;
				}
				else
				{
					var value = property.FindPropertyRelative("value");

					if (!value.hasVisibleChildren)
					{
						position.y += position.height;
					}

					position.height = EditorGUI.GetPropertyHeight(value, true);
					EditorGUI.PropertyField(position, value, new GUIContent(), true);
				}
			}
			else if (isEnabled.boolValue)
			{
				isEnabled.boolValue = false;
			}
		}
	}
}