using UnityEngine;
using System.Text.RegularExpressions;
using RenPy.Parser;

namespace RenPy.Script
{
	public class RenPySpeech : RenPyLine
	{
		private string m_character;
		public string Character {
			get { return m_character; }
		}
		
		private string m_text;
		public string Text {
			get { return m_text; }
		}
		
		public RenPySpeech(ref RenPyScanner tokens) : base(RenPyLineType.SPEECH) {
			m_character = tokens.Seek("\"").Trim();
			tokens.Next();
			m_text = tokens.Seek("\"").Replace("\\\"","\"");
			m_text = m_text.Replace("\n",""); // Remove extra newlines
			Regex trimmer = new Regex(@"\s\s+"); // Remove extra whitespace
			m_text = trimmer.Replace(m_text, " ");
			tokens.Next();
		}
		
		public override void Execute(RenPyDisplay display) {
			string str = m_character + " ";
			if(string.IsNullOrEmpty(m_character)) {
				str = "";
			}
			Static.LogRenPy(str + "\"" + m_text + "\"");
			
			if(Static.SkipDialog) {
				display.State.NextLine(display);
			}
			return;
		}
		
		public override string ToString() {
			return base.ToString() + ": " + (string.IsNullOrEmpty(m_character) ? "" : m_character + " ") + "\"" + m_text + "\"";
		}
	}
}
