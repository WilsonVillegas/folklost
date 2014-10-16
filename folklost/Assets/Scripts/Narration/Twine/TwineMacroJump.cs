using UnityEngine;
using System;
using System.Collections;
using Twine.Util;

namespace Twine {
	
	/// <summary>
	/// Represents a line of speech in Twine.
	/// </summary>
	public class TwineMacroJump : TwineLine
	{
		#region Properties
		
		private string m_targetPassage;
		
		/// <summary>
		/// The passage to jump to
		/// </summary>
		public string TargetPassage {
			get { return m_targetPassage; }
		}
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Parses a line from the two strings given to it.
		/// </summary>
		/// <param name="metadata">The metadata string</param>
		/// <param name="text">The line</param>
		public TwineMacroJump(ref Scanner scan) {
			scan.Next(); // Should be "jump"
			m_targetPassage = scan.Next();
			scan.Next(); // Should be ">>"
			scan.Next(); // Should be "\n"
		}
		
		#endregion
		
		#region Methods
		
		public override IEnumerator Execute(DialogState state) {
			yield return new WaitForEndOfFrame();
			state.SetCurrentPassage(m_targetPassage);
			state.Dialog.NextLineCoroutine();
		}
		
		public override void OnGUI() {
			// Nothing to do
		}
		
		#endregion
	}
}
