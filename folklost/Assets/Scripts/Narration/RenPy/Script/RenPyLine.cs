namespace RenPy.Script
{
	/// <summary>
	/// Represents a line in RenPy.
	/// </summary>
	public abstract class RenPyLine
	{
		/// <summary>
		/// The type of this line
		/// </summary>
		private RenPyLineType m_type;
		public RenPyLineType Type {
			get { return m_type; }
		}
		
		protected RenPyLine(RenPyLineType type) {
			this.m_type = type;
		}
		
		/// <summary>
		/// Executes the actions that this line takes.
		/// </summary>
		public abstract void Execute(RenPyDisplay state);
	}
}
