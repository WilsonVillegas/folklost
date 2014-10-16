using UnityEngine;
using System.Collections;
using Twine.Util;

namespace Twine {
	
	public class TwineConditional : TwineLine {
		
		#region Properties
		
		bool m_noTest = false;
		string m_testVariable, m_testValue;
		
		int m_targetLine;
		
		/// <summary>
		/// The line in the passage to jump to if the condition is false
		/// </summary>
		public int TargetLine {
			get { return m_targetLine; }
			set { m_targetLine = value; }
		}
		
		/// <summary>
		/// The variable to test
		/// </summary>
		public string TestVariable {
			get { return m_testVariable; }
		}
		
		/// <summary>
		/// The value to test
		/// </summary>
		public string TestValue {
			get { return m_testValue; }
		}
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="scan"></param>
		public TwineConditional(ref Scanner scan) {
			// Check what kind of conditional statement this is
			string mode = scan.Next(); // Should be "else" or "if"
			if(mode == "else") {
				if(scan.Peek() != "if") {
					m_noTest = true;
					scan.Next(); // Should be ">>"
					scan.Next(); // Should be "\n"
					return;
				} else  {
					scan.Next(); // Should be "if"
				}
			} else if (mode == "endif") {
				m_noTest = true;
				scan.Next(); // Should be ">>"
				scan.Next(); // Should be "\n"
				return;
			}
			
			// TODO: Support more complex if macros
			
			m_testVariable = scan.Next().Replace("$","");
			scan.Next();// Assume it is "is"
			m_testValue = scan.Next();
			
			scan.Next(); // Should be ">>"
			scan.Next(); // Should be '\n'
			
			// Default target line
			m_targetLine = -1;
		}
		
		#endregion
		
		#region Methods
		
		public bool EvaluatesTrue() {
			// Assume if the key does not exist that it is "false"
			if(!Twine.Variables.ContainsKey(m_testVariable)) {
				if("false" == m_testValue) {
					return true;
				}
			}
			// If the key exists and its value the same as the test value, return true
			else if(Twine.Variables[m_testVariable] == m_testValue) {
				return true;
			}
			return false;
		}

		public override IEnumerator Execute(DialogState state) {
			// Print debugging, if needed
			if(Twine.DebugLines) {
				Debug.Log("Title: "+ state.CurrentPassage.Title
				           + "  Line: " + state.CurrentLineIndex
				           + (m_noTest ? "\nNo conditional test\n\n"
				                       : "\nConditional evaluation if $" + m_testVariable
				                         + " is not " + m_testValue
				                         + " jump to " + m_targetLine
				                         + " ($" + m_testVariable
				                         + (Twine.Variables.ContainsKey(m_testVariable) ? " is \"" + Twine.Variables[m_testVariable] + "\"" : " does not exist")
				                         + ")\n\n"));
			}
			
			// Check if evaluation is required
			if(m_noTest) {
				JumpNextLine(ref state);
			}
			
			// If the condition is true, we can go to the next line
			else if(EvaluatesTrue()) {
				JumpNextLine(ref state);
			} else {
				JumpFalse(ref state);
			}
			
			yield return null;
		}
		
		/// <summary>
		/// Jump to the next line.
		/// </summary>
		public void JumpNextLine(ref DialogState state) {
			// Go to the next line
			state.CurrentLineIndex++;
			if(state.CurrentLineIndex < state.CurrentPassage.Lines.Length) {
				state.Dialog.NextLineCoroutine();
			}
				
			// If there is no next line, show the choices
			else {
				state.ShowChoices = true;
			}
		}
		
		/// <summary>
		/// Jump to the correct location in the passage if the evaluation is
		/// false.
		/// </summary>
		public void JumpFalse(ref DialogState state) {
			// If the target line is < 0, print a warning and go to the next line
			int target = m_targetLine;
			if(m_targetLine < 0) {
				target = state.CurrentLineIndex + 1;
				Debug.LogWarning("Target Line was not set!");
			}
			
			// Jump to the target line
			state.CurrentLineIndex = target;
			if(state.CurrentLineIndex < state.CurrentPassage.Lines.Length) {
				state.Dialog.NextLineCoroutine();
			}
			
			// If there is no next line, show the choices
			else {
				state.ShowChoices = true;
			}
		}
		
		public override void OnGUI() {
			// Nothing to do
		}
		
		#endregion
	}
}
