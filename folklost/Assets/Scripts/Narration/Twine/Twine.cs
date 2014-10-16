#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;

namespace Twine {
	
	/// <summary>
	/// Static Twine data.
	/// </summary>
	#if UNITY_EDITOR
	[InitializeOnLoad]
	public class Twine : EditorWindow
	#else
	public class Twine
	#endif
	{
		#region Static Debug Variables
		#if UNITY_EDITOR
		
		private static bool m_debug;
		private static bool m_skipDialog;
		private static bool m_debugLines;
		
		/// <summary>
		/// Whether or not to show debugging information
		/// </summary>
		public static bool DebugMode {
			get { return m_debug; }
		}
		
		/// <summary>
		/// Whether or not to skip the dialog and go straight to the choices
		/// </summary>
		public static bool SkipDialog {
			get { return m_debug && m_skipDialog; }
		}
		
		/// <summary>
		/// Whether or not to print debugging information for each line
		/// </summary>
		public static bool DebugLines {
			get { return m_debug && m_debugLines; }
		}
		
		#else
		
		public static bool DebugMode {
			get { return false; }
		}
		
		public static bool SkipDialog {
			get { return false; }
		}
		
		public static bool DebugLines {
			get { return false; }
		}
		
		#endif
		#endregion
		
		#region Static Variables
		
		private static Dictionary<string, string> m_variables;
		
		public static Dictionary<string, string> Variables {
			get {
				m_variables = m_variables ?? new Dictionary<string,string>();
				return m_variables;
			}
		}
		
		#endregion
		
		#if UNITY_EDITOR
		
		#region Unity UI
		
		[MenuItem("Window/Twine")]
		public static void ShowWindow()
		{
			EditorWindow window = EditorWindow.GetWindow(typeof(Twine));
			window.title = "Twine";
		}
		
		void OnGUI() {
			bool oldDebug = m_debug;
			bool oldSkip = m_skipDialog;
			bool oldLines = m_debugLines;
			
			m_debug = EditorGUILayout.BeginToggleGroup("Enable Debugging", m_debug);
				m_skipDialog = EditorGUILayout.Toggle("Skip Dialog", m_skipDialog);
				m_debugLines = EditorGUILayout.Toggle("Debug Lines", m_debugLines);
			EditorGUILayout.EndToggleGroup ();
			
			// Save if any options have changed
			if(oldDebug != m_debug || oldSkip != m_skipDialog || oldLines != m_debugLines) {
				SaveEditorPrefs();
			}
		}
		
		#endregion
		
		#region Unity Editor Prefs
		
		/// <summary>
		/// Load this window's preferences when Unity is loaded.
		/// </summary>
		static Twine() {
			LoadEditorPrefs();
		}
		
		/// <summary>
		/// Load this window's preferences when this window gains focus.
		/// </summary>
		void OnFocus() {
			LoadEditorPrefs();
		}
		
		/// <summary>
		/// Loads this window's preferences by querying EditorPrefs.
		/// </summary>
		static void LoadEditorPrefs() {
			m_debug = EditorPrefs.GetBool("twine-m_debug",false);
			m_skipDialog = EditorPrefs.GetBool("twine-m_skipDialog",false);
			m_debugLines = EditorPrefs.GetBool("twine-m_debugLines",false);
		}
		
		/// <summary>
		/// Saves this window's preferences by using EditorPrefs.
		/// </summary>
		static void SaveEditorPrefs() {
			if(m_debug) {
				EditorPrefs.SetBool("twine-m_debug", m_debug);
			} else {
				EditorPrefs.DeleteKey("twine-m_debug");
			}
			if(m_skipDialog) {
				EditorPrefs.SetBool("twine-m_skipDialog", m_skipDialog);
			} else {
				EditorPrefs.DeleteKey("twine-m_skipDialog");
			}
			if(m_debugLines) {
				EditorPrefs.SetBool("twine-m_debugLines", m_debugLines);
			} else {
				EditorPrefs.DeleteKey("twine-m_debugLines");
			}
		}
		
		#endregion
		
		#endif
	}
}
