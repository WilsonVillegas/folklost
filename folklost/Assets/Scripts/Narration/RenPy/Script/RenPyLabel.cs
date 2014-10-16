using RenPy.Parser;

namespace RenPy.Script
{
	public class RenPyLabel : RenPyLine
	{
		private string m_name;
		public string Name {
			get { return m_name; }
		}
		
		public RenPyLabel(ref RenPyScanner tokens) : base(RenPyLineType.LABEL) {
			tokens.Seek("label");
			tokens.Next();
			m_name = tokens.Seek(":").Trim();
			tokens.Next();
		}
		
		public override void Execute(RenPyDisplay display) {
			Static.LogRenPy("label " + m_name + ":");
		}
		
		public override string ToString() {
			return base.ToString() + ": label \"" + m_name + "\"";
		}
	}
}
