#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;

namespace RenPy.Editor {
	
	[CustomEditor(typeof(RenPyDisplay),true)]
	public class RenPyDisplayEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			RenPyDisplay display = (RenPyDisplay) target;
			
			GUILayout.Space(5);
			GUILayout.Label("Debug Controls:");
			
			GUI.enabled = UnityEngine.Application.isPlaying && Static.DebugMode;
			if(GUILayout.Button("Start Dialog")) {
				display.StartDialog();
			}
		}
	}
}

#endif