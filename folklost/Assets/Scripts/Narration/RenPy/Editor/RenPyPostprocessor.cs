#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using RenPy.Parser;

namespace RenPy.Editor
{
	public class RenPyPostprocessor : AssetPostprocessor
	{
		/// <summary>
		/// Finds script.rpy files and saves them to a custom asset files.
		/// </summary>
		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath) {
			foreach(string assetPath in importedAssets) {
				CreateRenPyAsset(GetRenPyFileHandle(assetPath));
			}
			
			foreach(string assetPath in deletedAssets) {
				RemoveRenPyAsset(GetRenPyFileHandle(assetPath, false));
			}
			
			foreach(string assetPath in movedAssets) {
				CreateRenPyAsset(GetRenPyFileHandle(assetPath));
			}
			
			foreach(string assetPath in movedFromPath) {
				RemoveRenPyAsset(GetRenPyFileHandle(assetPath, false));
			}
		}
		
		private static void RemoveRenPyAsset(RenPyFileHandle handle) {
			if(handle != null) {
				AssetDatabase.DeleteAsset(handle.path);
			}
		}
		
		private static void CreateRenPyAsset(RenPyFileHandle handle) {
			if(handle != null) {
				RenPyScriptAsset script = ScriptableObject.CreateInstance<RenPyScriptAsset>();
				script.Title = handle.name;
				script.Lines = handle.lines;
				
				RenPyScriptAsset outputScript = AssetDatabase.LoadMainAssetAtPath(handle.path) as RenPyScriptAsset;
				if (outputScript != null) {
					EditorUtility.CopySerialized(script, outputScript);
					AssetDatabase.SaveAssets ();
				} else {
					AssetDatabase.CreateAsset(script, handle.path);
					AssetDatabase.SaveAssets();
				}
			}
		}
		
		private static RenPyFileHandle GetRenPyFileHandle(string assetPath, bool getContents = true) {
			// Check if the file is a Ren'Py script
			string filename = Path.GetFileNameWithoutExtension(assetPath);
			string filetype = Path.GetExtension(assetPath);
			if(filetype != ".rpy" || filename != "script") {
				return null;
			}
			
			// Get the folder name
			string foldername = Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(assetPath)));
			
			// Get the asset's path
			string path = Path.GetDirectoryName(assetPath);
			path += Path.DirectorySeparatorChar + "script-" + foldername + ".asset";
			
			// Get the file's contents
			List<string> lines = new List<string>();
			if(getContents) {
				StreamReader scanner = new StreamReader(assetPath);
				while(scanner.Peek() > 0) {
					string line = scanner.ReadLine();
					lines.Add(line);
				}
			}
			
			// Return a Ren'Py handle
			return new RenPyFileHandle(foldername, path, lines.ToArray());
		}
	}
}

#endif
