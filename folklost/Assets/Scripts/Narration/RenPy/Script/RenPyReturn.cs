using RenPy.Parser;

namespace RenPy.Script
{
	public class RenPyReturn : RenPyLine
	{
		public RenPyReturn(ref RenPyScanner tokens) : base(RenPyLineType.RETURN) {
			tokens.Seek("return");
			tokens.Next();
		}
		
		public override void Execute(RenPyDisplay display) {
			Static.LogRenPy("return");
			display.State.End();
		}
		
		public override string ToString() {
			return base.ToString() + ": return";
		}
	}
}
