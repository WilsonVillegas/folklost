using RenPy.Parser;

namespace RenPy.Script
{
	public class RenPyComment : RenPyLine
	{
		private string m_comment;
		
		public RenPyComment(ref RenPyScanner tokens) : base(RenPyLineType.COMMENT) {
			tokens.Seek("#");
			tokens.Next();
			m_comment = tokens.Seek("\n");
		}
		
		public override void Execute(RenPyDisplay display) {
			return;
		}
		
		public override string ToString() {
			return base.ToString() + ": #" + m_comment;
		}
	}
}
