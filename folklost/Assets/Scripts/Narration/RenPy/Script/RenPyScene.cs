using RenPy.Parser;

namespace RenPy.Script
{
	public class RenPyScene : RenPyLine
	{
		private string m_imageVarName;
		
		public RenPyScene(ref RenPyScanner tokens) : base(RenPyLineType.SCENE) {
			tokens.Seek("scene");
			tokens.Next();
			
			m_imageVarName = tokens.Seek(new string[] {"\n", "with"}).Trim();
			
			// Check if there is a "with" token next and ignore it
			if(tokens.PeekIgnoreWhitespace() == "with") {
				tokens.Seek("with");
				tokens.Next();
				tokens.Next(); // Ignore the argument for with
			}
		}
		
		public override void Execute(RenPyDisplay display) {
			return;
		}
		
		public override string ToString() {
			return base.ToString() + ": scene \"" + m_imageVarName + "\"";
		}
	}
}
