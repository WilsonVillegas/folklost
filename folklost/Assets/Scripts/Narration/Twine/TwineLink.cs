using System;
using Twine.Util;

namespace Twine {
	
	/// <summary>
	/// Represents a link in a twine passage.
	/// </summary>
	public class TwineLink
	{
		#region Properties
		
		private string m_text;
		private string m_target;
		
		private bool m_setVariable;
		private string m_variable;
		private string m_value;
		
		private TwineConditional m_conditional;
		
		/// <summary>
		/// The text for the link
		/// </summary>
		public string Text {
			get { return m_text; }
		}
		
		/// <summary>
		/// The name of the passage that this link targets
		/// </summary>
		public string Target {
			get { return m_target; }
		}
		
		/// <summary>
		/// Whether or not this link sets a variable when selected
		/// </summary>
		public bool SetsVariable {
			get { return m_setVariable; }
		}
		
		/// <summary>
		/// The variable this link sets
		/// </summary>
		public string Variable {
			get { return m_variable; }
		}
		
		/// <summary>
		/// The value this link sets the variable to
		/// </summary>
		public string Value {
			get { return m_value; }
		}
		
		/// <summary>
		/// The conditional to test whether or not to show this option
		/// </summary>
		public TwineConditional Conditional {
			get { return m_conditional; }
			set { m_conditional = value; }
		}
		
		/// <summary>
		/// Whether or not to show this option
		/// </summary>
		public bool Show {
			get { return m_conditional == null || m_conditional.EvaluatesTrue(); }
		}
		
		#endregion
		
		/// <summary>
		/// Creates a new TwineLink.
		/// </summary>
		/// <param name="text">The link source code</param>
		public TwineLink(ref Scanner scan) {
			scan.Next(); // Should be [[
			
			// Parse text
			while(scan.HasNext()) {
				string token = scan.Next();
				if(token == "|") {
					break;
				} else if(token == "]]") {
					m_target = m_text;
					scan.Next(); // Should be "\n"
					return;
				}
				
				if(m_text == null) {
					m_text = token;
				} else {
					m_text += " " + token;
				}
			}
			
			// Parse target
			while(scan.HasNext()) {
				string token = scan.Next();
				if(token == "]]") {
					break;
				}
				if(token == "]") {
					m_setVariable = true;
					scan.Next(); // Should be '['
					m_variable = scan.Next().Replace("$","");
					scan.Next(); // Should be '='
					m_value = scan.Next();
					scan.Next(); // Should be ']]'
					break;
				}
				if(m_target == null) {
					m_target = token;
				} else {
					m_target += " " + token;
				}
			}
			scan.Next(); // Should be '\n'
		}
	}
}
