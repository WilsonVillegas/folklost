#if UNITY_EDITOR

namespace RenPy.Editor
{
	/// <summary>
	/// Class that stores information about a Ren'Py script
	/// </summary>
	public class RenPyFileHandle
	{
		/// <summary>
		/// The name of the Ren'Py script
		/// </summary>
		public readonly string name;
		
		/// <summary>
		/// The path to the Ren'Py script
		/// </summary>
		public readonly string path;

		/// <summary>
		/// The content of the Ren'Py script
		/// </summary>
		public readonly string[] lines;
		
		public RenPyFileHandle(string name, string path, string[] lines) {
			this.name = name;
			this.path = path;
			this.lines = lines;
		}
	}
}

#endif
