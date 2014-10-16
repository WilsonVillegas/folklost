using System.Collections.Generic;
using RenPy.Script;

namespace RenPy.Dialog
{
	public class RenPyDialogState
	{
		private readonly string m_name;
		public string Name {
			get { return m_name; }
		}
		private readonly RenPyLine[] m_lines;
		
		public string ResourcePath {
			get { return "Narrative/"+m_name+"/game/"; }
		}
		
		private Dictionary<string, RenPyCharacter> m_characters;
		private Dictionary<string, int> m_labels;
		
		private int m_currLineIndex;
		private bool m_reachedEnd;
		public bool ReachedEnd {
			get { return m_reachedEnd; }
		}

		public RenPyLine CurrentLine {
			get {
				if(m_reachedEnd || m_currLineIndex >= m_lines.Length || m_currLineIndex < 0) {
					return null;
				}
				return m_lines[m_currLineIndex];
			}
		}
		
		public RenPyDialogState(string name, RenPyLine[] lines) {
			this.m_name = name;
			this.m_lines = lines;
			Reset();
		}
		
		public RenPyCharacter GetCharacter(string characterVarName) {
			return m_characters[characterVarName];
		}
		
		public void AddCharacter(RenPyCharacter character) {
			m_characters.Add(character.VarName, character);
		}
		
		public string GetVariable(string name) {
			return Static.Variables.ContainsKey(name) ? Static.Variables[name] : "";
		}
		
		public void SetVariable(string name, string value) {
			if(!Static.Variables.ContainsKey(name)) {
				Static.Variables.Add(name, value);
			} else {
				Static.Variables[name] = value;
			}
		}
		
		public void NextLine(RenPyDisplay display, bool execute=true) {
			m_currLineIndex++;
			if(execute && CurrentLine != null) {
				CurrentLine.Execute(display);
			}
		}
		
		public bool GoToLabel(RenPyDisplay display, string label) {
			if(m_labels.ContainsKey(label)) {
				m_currLineIndex = m_labels[label];
				CurrentLine.Execute(display);
				return true;
			}
			return false;
		}
		
		public void End() {
			m_reachedEnd = true;
		}
		
		public void Reset() {
			this.m_characters = new Dictionary<string,RenPyCharacter>();
			this.m_labels = new Dictionary<string,int>();
			for(int i = 0; i < m_lines.Length; i++) {
				RenPyLabel label = m_lines[i] as RenPyLabel;
				if(label == null) {
					continue;
				} else {
					m_labels.Add(label.Name, i);
				}
			}
			
			m_currLineIndex = 0;
			m_reachedEnd = false;
		}
	}
}
