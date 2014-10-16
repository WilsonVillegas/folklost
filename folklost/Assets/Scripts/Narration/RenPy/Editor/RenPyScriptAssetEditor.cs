#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using RenPy.Parser;

namespace RenPy.Editor
{
	/// <summary>
	/// Displays Ren'Py Script Assets in the editor.
	/// </summary>
	[CustomEditor(typeof(RenPyScriptAsset))]
	public class RenPyScriptAssetEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI() {
			RenPyScriptAsset script = target as RenPyScriptAsset;
			if (null == script) {
				return;
			}
			
			// If the Script Asset is empty, display a help box
			if(script.Lines == null || script.Lines.Length == 0) {
				EditorGUILayout.HelpBox("Script is empty!", MessageType.Error);
			}
			
			// Otherwise, display the Script Asset's contents
			else {
				string str = "";
				for(int i = 0; i < script.Lines.Length; i++) {
					str += script.Lines[i] + "\n";
				}
				GUIStyle style = new GUIStyle();
				style.normal.textColor = Color.white;
				GUILayout.Label(str, style);
			}
		}
	}
}

#endif
