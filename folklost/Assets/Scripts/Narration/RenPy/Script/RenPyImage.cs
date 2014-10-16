using RenPy.Parser;

namespace RenPy.Script
{
	public class RenPyImage : RenPyLine
	{
		private string m_varName;
		public string VarName {
			get { return m_varName; }
		}
		
		private string m_imageName;
		public string ImageName {
			get { return m_imageName; }
		}
		
		public RenPyImage(ref RenPyScanner tokens) : base(RenPyLineType.IMAGE) {
			tokens.Seek("image");
			tokens.Next();
			m_varName = tokens.Seek("=").Trim();
			tokens.Next();
			
			tokens.Seek("\"");
			tokens.Next();
			m_imageName = tokens.Seek("\"");
			tokens.Next();
		}
		
		public override void Execute(RenPyDisplay display) {
			return;
		}
		
		public override string ToString() {
			return base.ToString() + ": \"" + m_varName + "\" = \"" + m_imageName + "\"";
		}
	}
}
