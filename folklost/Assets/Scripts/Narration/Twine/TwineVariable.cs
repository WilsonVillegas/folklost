using UnityEngine;
using System.Collections;
using Twine.Util;

namespace Twine {
	
	public class TwineVariable : TwineLine {
		
		#region Properties
		
		private string m_variable;
		private string m_value;
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Parses a line from the two strings given to it.
		/// </summary>
		/// <param name="metadata">The metadata string</param>
		/// <param name="text">The line</param>
		public TwineVariable(ref Scanner scan) {
			scan.Next(); // Should be "set"
			
			m_variable = scan.Next().Replace("$","");
			scan.Next(); // Should be "to"
			m_value = scan.Next();

			scan.Next(); // Should be ">>"
			scan.Next(); // Should be '\n'
		}
		
		#endregion
		
		#region Methods
		
		public override IEnumerator Execute(DialogState state) {
			// Set the variable
			if(Twine.Variables.ContainsKey(m_variable)) {
				Twine.Variables[m_variable] = m_value;
			} else {
				Twine.Variables.Add(m_variable, m_value);
			}
			
			// Print debugging, if needed
			if(Twine.DebugLines) {
				Debug.Log("Title: "+ state.CurrentPassage.Title
				           + "  Line: " + state.CurrentLineIndex
				           + "\nSet variable $" + m_variable
				           + " to " + m_value + "\n\n");
			}
			
			// Go to the next line
			state.CurrentLineIndex++;
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
		
		#endregion
	}
}
