using Attributes;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RestrictAttribute))]
public class RestrictAttributeDrawer : PropertyDrawer
{
    private static string _notReferenceErrorMessage = "Property is not a reference type";
    private bool _isTooltipSet = false;
    private string tooltip = "Should has script with ";
    private static List<KeyValuePair<string, bool>> keyValuePairs = new List<KeyValuePair<string, bool>>();
    private int _index = -1;
    const float helpHeight = 30f;
    const float fieldHeight = 16f;
    private bool _hasWrongValue
    {
        get
        {
            if (_index < 0)
            {
                return false;
            }
            return keyValuePairs[_index].Value;
        }
        set
        {
            var newValue = new KeyValuePair<string, bool>(keyValuePairs[_index].Key, value);
            keyValuePairs[_index] = newValue;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (_hasWrongValue)
        {
            return base.GetPropertyHeight(property, label) + helpHeight;
        }
        return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_index < 0)
        {
            UpdateHasWrongValue(property);
        }

        if (property.propertyType != SerializedPropertyType.ObjectReference)
        {
            EditorGUI.LabelField(position, label, new GUIContent(_notReferenceErrorMessage));
            return;
        }
        var restrictAttribute = this.attribute as RestrictAttribute;

        if (!_isTooltipSet)
        {
            label.tooltip = tooltip;
            _isTooltipSet = true;
        }

        if (property.objectReferenceValue != null && !property.objectReferenceValue.GetComponent(restrictAttribute.RequiredType))
        {
            _hasWrongValue = true;
        }
        else if (property.objectReferenceValue != null && property.objectReferenceValue.GetComponent(restrictAttribute.RequiredType))
        {
            _hasWrongValue = false;
        }

        if (_hasWrongValue)
        {
            property.objectReferenceValue = null;
        }

        EditorGUI.BeginProperty(position, label, property);
        Rect newRect = new Rect(position.x, position.y + 20, position.width, position.height);

        if (_hasWrongValue)
        {
            position.height -= helpHeight;
            property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(GameObject), true);
            DrawBox(position, restrictAttribute);
        }
        else
            property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(GameObject), true);
        EditorGUI.EndProperty();
    }

    private void DrawBox(Rect position, RestrictAttribute restrictAttribute)
    {
        Rect helpPosition = EditorGUI.IndentedRect(position);
        helpPosition.y += fieldHeight;
        helpPosition.height = helpHeight;
        EditorGUI.HelpBox(helpPosition, RestrictAttribute.NotImplementInterfaceErrorMessage, MessageType.Warning);
    }

    private void UpdateHasWrongValue(SerializedProperty property)
    {
        int index = keyValuePairs.FindIndex(pair => pair.Key == property.propertyPath);

        if (index > -1)
        {
            _index = index;
        }
        else
        {
            keyValuePairs.Add(new KeyValuePair<string, bool>(property.propertyPath, false));
            _index = keyValuePairs.Count - 1;
        }
    }
}