#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;

namespace RenPy {
	
	/// <summary>
	/// Static Twine data.
	/// </summary>
	#if UNITY_EDITOR
	[InitializeOnLoad]
	public class Static : EditorWindow
	#else
	public class Static
	#endif
	{
		#region Static Debug Variables
		#if UNITY_EDITOR
		
		private static bool m_debug;
		private static bool m_skipDialog;
		private static bool m_debugLines;
		private static bool m_debugExternal;
		private static bool m_run;
		
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
		private static bool DebugLines {
			get { return m_debug && m_debugLines; }
		}
		
		/// <summary>
		/// Whether or not to print debugging information related to Ren'Py
		/// when actions in the game are taken.
		/// </summary>
		private static bool DebugAction {
			get { return m_debug && m_debugExternal; }
		}
		
		/// <summary>
		/// Whether or not to allow fast movement.
		/// </summary>
		public static bool Run {
			get { return m_debug && m_run; }
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
		
		public static bool DebugAction {
			get { return false; }
		}
		
		public static bool Run {
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
		
		[MenuItem("Window/RenPy")]
		public static void ShowWindow()
		{
			EditorWindow window = EditorWindow.GetWindow(typeof(Static));
			window.title = "RenPy";
		}
		
		void OnGUI() {
			bool oldDebug = m_debug;
			bool oldSkip = m_skipDialog;
			bool oldLines = m_debugLines;
			bool oldExternal = m_debugExternal;
			bool oldRun= m_run;
			
			m_debug = EditorGUILayout.BeginToggleGroup("Enable Debugging", m_debug);
				m_skipDialog = EditorGUILayout.Toggle("Skip Dialog", m_skipDialog);
				m_debugLines = EditorGUILayout.Toggle("Debug Lines", m_debugLines);
				m_debugExternal = EditorGUILayout.Toggle("Debug External", m_debugExternal);
				m_run = EditorGUILayout.Toggle("Run", m_run);
			EditorGUILayout.EndToggleGroup ();
			
			// Save if any options have changed
			if(oldDebug != m_debug || oldSkip != m_skipDialog || oldLines != m_debugLines || oldExternal != m_debugExternal || oldRun != m_run) {
				SaveEditorPrefs();
			}
		}
		
		#endregion
		
		#region Unity Editor Prefs
		
		/// <summary>
		/// Load this window's preferences when Unity is loaded.
		/// </summary>
		static Static() {
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
			m_debug = EditorPrefs.GetBool("renpy-m_debug", true);
			m_skipDialog = EditorPrefs.GetBool("renpy-m_skipDialog", false);
			m_debugLines = EditorPrefs.GetBool("renpy-m_debugLines", false);
			m_debugExternal = EditorPrefs.GetBool("renpy-m_debugExternal", false);
			m_run = EditorPrefs.GetBool("renpy-m_run", false);
		}
		
		/// <summary>
		/// Saves this window's preferences by using EditorPrefs.
		/// </summary>
		static void SaveEditorPrefs() {
			if(m_debug) {
				EditorPrefs.SetBool("renpy-m_debug", m_debug);
			} else {
				EditorPrefs.DeleteKey("renpy-m_debug");
			}
			if(m_skipDialog) {
				EditorPrefs.SetBool("renpy-m_skipDialog", m_skipDialog);
			} else {
				EditorPrefs.DeleteKey("renpy-m_skipDialog");
			}
			if(m_debugLines) {
				EditorPrefs.SetBool("renpy-m_debugLines", m_debugLines);
			} else {
				EditorPrefs.DeleteKey("renpy-m_debugLines");
			}
			if(m_debugExternal) {
				EditorPrefs.SetBool("renpy-m_debugExternal", m_debugExternal);
			} else {
				EditorPrefs.DeleteKey("renpy-m_debugExternal");
			}
			if(m_run) {
				EditorPrefs.SetBool("renpy-m_run", m_run);
			} else {
				EditorPrefs.DeleteKey("renpy-m_run");
			}
		}
		
		#endregion
		
		#endif

		#region Logging
		
		public static void LogRenPy(string message) {
			string msg = "[RENPY] " + message;
			if(DebugLines) {
				Debug.Log(msg);
			}
			if(!Log.HasNotStarted()) {
				Log.Write(msg);
			}
		}
		
		public static void LogAction(string message) {
			string msg = "[RENPY] " + message;
			if(DebugAction) {
				Debug.Log(msg);
			}
			if(!Log.HasNotStarted()) {
				Log.Write(msg);
			}
		}
		
		#endregion
	}
}
