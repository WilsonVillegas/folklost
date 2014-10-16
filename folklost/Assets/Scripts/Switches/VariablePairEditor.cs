using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(VariablePair))]
public class VariablePairEditor : PropertyDrawer {
	
	public override void OnGUI (Rect rect, SerializedProperty property, GUIContent label) {
		label = EditorGUI.BeginProperty(rect, label, property);
		float width = rect.width;
		
		rect.width = width * .1f;
		EditorGUI.LabelField(rect, "K");
		rect.x += rect.width;
		rect.width = width * .4f;
		EditorGUI.PropertyField(rect, property.FindPropertyRelative("key"), GUIContent.none);
		rect.x += rect.width;
		
		rect.width = width * .1f;
		EditorGUI.LabelField(rect, "V");
		rect.x += rect.width;
		rect.width = width * .4f;
		EditorGUI.PropertyField(rect, property.FindPropertyRelative("value"), GUIContent.none);
		rect.x += rect.width;
		
		EditorGUI.EndProperty();
	}
}

#endif