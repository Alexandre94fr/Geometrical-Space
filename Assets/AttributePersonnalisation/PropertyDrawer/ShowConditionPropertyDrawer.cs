using System.Reflection;
using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ShowConditionAttribute))]
public class ShowConditionPropertyDrawer : PropertyDrawer
{
    
    private enum Visibility
    {
        Visible,
        Disabled,
        Hidden
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        bool oldGuiEnabled = GUI.enabled;

        Visibility visibility = GetVisibility(property);
        if (visibility is Visibility.Visible or Visibility.Disabled)
        {
            if (visibility == Visibility.Disabled)
            {
                GUI.enabled = false;
            }

            EditorGUI.PropertyField(position, property);
            if (visibility == Visibility.Disabled)
            {
                GUI.enabled = oldGuiEnabled;
            }
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Visibility visibility = GetVisibility(property);
        if (visibility is Visibility.Visible or Visibility.Disabled)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        return -EditorGUIUtility.standardVerticalSpacing;
    }

    private Visibility GetVisibility(SerializedProperty property)
    {
        ShowConditionAttribute conditionAttribute = (ShowConditionAttribute)attribute;

        if (conditionAttribute == null)
        {
            return Visibility.Visible;
        }

        string condition = conditionAttribute.Condition;
        SerializedObject serializedObject = property.serializedObject;

        if (serializedObject == null)
        {
            return Visibility.Visible;
        }
        object target = PropertyDrawerExtensionMethods.GetTargetObjectOfProperty(property);
        Type test = target.GetType();
        FieldInfo conditionProperty = test.GetField(condition);


        if (conditionProperty == null)
        {
            EditorGUILayout.HelpBox(
                $"The condition used on the HideCondition ({condition}) can't be serialized.",
                MessageType.Error);
            return Visibility.Visible;
        }

        if (conditionProperty.FieldType != typeof(bool))
        {
            EditorGUILayout.HelpBox(
                $"The condition used on the HideCondition ({condition}) attribute is not a boolean.",
                MessageType.Error);
            return Visibility.Visible;
        }

        if ((bool)conditionProperty.GetValue(target))
        {
            return Visibility.Visible;
        }

        if (conditionAttribute.Disable)
        {
            return Visibility.Disabled;
        }

        return Visibility.Hidden;
    }
    
}
#endif