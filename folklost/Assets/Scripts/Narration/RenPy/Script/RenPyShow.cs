using RenPy.Parser;

namespace RenPy.Script
{
	public class RenPyShow : RenPyLine
	{
		private string m_characterVarName;
		
		public RenPyShow(ref RenPyScanner tokens) : base(RenPyLineType.SHOW) {
			tokens.Seek("show");
			tokens.Next();
			
			m_characterVarName = tokens.Seek(new string[] {"\n", "with"}).Trim();
			
			// Check if there is a "with" token next and ignore it
			tokens.SkipWhitespace(true,true,true);
			if(tokens.Peek() == "with") {
				tokens.Seek("\n");
			}
		}
		
		public override void Execute(RenPyDisplay display) {
			return;
		}
		
		public override string ToString() {
			return base.ToString() + ": show \"" + m_characterVarName + "\"";
		}
	}
}
