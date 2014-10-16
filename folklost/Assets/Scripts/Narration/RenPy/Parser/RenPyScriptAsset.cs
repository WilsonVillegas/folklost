using UnityEngine;

namespace RenPy.Parser
{
	/// <summary>
	/// Stores information about a Ren'Py script.
	/// </summary>
	[System.Serializable]
	public class RenPyScriptAsset: ScriptableObject
	{
		public string Title;
		public string[] Lines;
	}
}
