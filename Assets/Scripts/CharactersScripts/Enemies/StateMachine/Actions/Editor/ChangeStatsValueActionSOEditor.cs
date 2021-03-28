using UnityEditor;
using UnityEngine;
using Pawny.StateMachine;

[CustomEditor(typeof(ChangeStatsValueActionSO)), CanEditMultipleObjects]
public class ChangeStatsValueActionSOEditor : CustomBaseEditor
{
	public override void OnInspectorGUI()
	{
		DrawNonEdtiableScriptReference<ChangeStatsValueActionSO>();

		serializedObject.Update();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("whenToRun"));
		EditorGUILayout.Space();

		EditorGUILayout.LabelField("Stats Property", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("propertyName"), new GUIContent("Name"));

		// Draws the appropriate value depending on the type of parameter this SO is going to change on the Animator
		SerializedProperty statsPropertyValue = serializedObject.FindProperty("propertyType");

		EditorGUILayout.PropertyField(statsPropertyValue, new GUIContent("Type"));

		switch (statsPropertyValue.intValue)
		{
			case (int)ChangeStatsValueActionSO.PropertyType.Bool:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("boolValue"), new GUIContent("Desired value"));
				break;
			case (int)ChangeStatsValueActionSO.PropertyType.Int:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("intValue"), new GUIContent("Desired value"));
				break;
			case (int)ChangeStatsValueActionSO.PropertyType.Float:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("floatValue"), new GUIContent("Desired value"));
				break;

		}

		serializedObject.ApplyModifiedProperties();
	}
}
