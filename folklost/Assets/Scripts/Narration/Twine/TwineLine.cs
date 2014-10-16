using System.Collections;

namespace Twine {
	
	/// <summary>
	/// Represents a line in Twine.
	/// </summary>
	public abstract class TwineLine
	{
		/// <summary>
		/// Executes the actions that this TwineLine takes.
		/// </summary>
		public abstract IEnumerator Execute(DialogState state);
		
		/// <summary>
		/// Renders the required GUI for this TwineLine.
		/// </summary>
		public abstract void OnGUI();
	}
}
