using UnityEngine;
using RenPy.Parser;

namespace RenPy.Script
{
	public class RenPyJump : RenPyLine
	{
		private string m_target;
		
		public RenPyJump(ref RenPyScanner tokens) : base(RenPyLineType.JUMP) {
			tokens.Seek("jump");
			tokens.Next();
			m_target = tokens.Seek("\n").Trim();
			tokens.Next();
		}
		
		public override void Execute(RenPyDisplay display) {
			Static.LogRenPy("jump " + m_target);
			
			
			bool success = display.State.GoToLabel(display, m_target);
			if(!success) {
				Debug.LogError("Could not find the label \"" + m_target + "\"");
			}
		}
		
		public override string ToString() {
			return base.ToString() + ": jump \"" + m_target + "\"";
		}
	}
}
