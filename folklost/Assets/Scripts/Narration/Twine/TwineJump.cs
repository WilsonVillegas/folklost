using UnityEngine;
using System.Collections;

namespace Twine {
	
	public class TwineJump : TwineLine {
		
		private int m_targetLine = -1;
		
		/// <summary>
		/// The line in the passage to jump to if the condition is false
		/// </summary>
		public int TargetLine {
			get { return m_targetLine; }
			set { m_targetLine = value; }
		}
		
		public override IEnumerator Execute(DialogState state) {
			// Print debugging, if needed
			if(Twine.DebugLines) {
				Debug.Log("Title: "+ state.CurrentPassage.Title
				           + "  Line: " + state.CurrentLineIndex
				           + "\nJumping to line " + m_targetLine + "\n\n");
			}
			
			// Jump to the target line
			state.CurrentLineIndex = m_targetLine;
			if(state.CurrentLineIndex < state.CurrentPassage.Lines.Length) {
				state.Dialog.NextLineCoroutine();
			}
			
			// If there is no next line, show the choices
			else {
				state.ShowChoices = true;
			}
			
			yield return null;
		}
		
		public override void OnGUI() {
			// Nothing to do
		}
	}
}
