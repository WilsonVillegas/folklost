using UnityEngine;
using System.Collections;

namespace Twine {
	
	public class DialogState {
		
		private Dialog m_dialog;
		private TwineStory m_story;
		private TwinePassage m_currentPassage;
		private int m_currentLineIndex;
		private int m_lastSpeechLineIndex;
		private bool m_showChoices;
		
		/// <summary>
		/// The Dialog script that owns this state.
		/// </summary>
		public Dialog Dialog {
			get { return m_dialog; }
			set { m_dialog = value; }
		}
		
		/// <summary>
		/// The imported twine story data.
		/// </summary>
		public TwineStory Story {
			get { return m_story; }
			set { m_story = value; }
		}
		
		/// <summary>
		/// The current passage being read from the story.
		/// </summary>
		public TwinePassage CurrentPassage {
			get { return m_currentPassage; }
			set { m_currentPassage = value; }
		}
		
		/// <summary>
		/// The current line in the passage.
		/// </summary>
		public TwineLine CurrentLine {
			get {
				int index = Mathf.Clamp(m_currentLineIndex, 0, m_currentPassage.Lines.Length-1);
				return m_currentPassage.Lines[index];
			}
		}
		
		/// <summary>
		/// The current line in the passage.
		/// </summary>
		public TwineLine LastSpeechLine {
			get { return m_currentPassage.Lines[m_lastSpeechLineIndex]; }
		}
		
		/// <summary>
		/// The current line index in the passage.
		/// </summary>
		public int CurrentLineIndex {
			get { return m_currentLineIndex; }
			set { m_currentLineIndex = value; }
		}
		
		/// <summary>
		/// The current line index in the passage.
		/// </summary>
		public int LastSpeechLineIndex {
			get { return m_lastSpeechLineIndex; }
			set { m_lastSpeechLineIndex = value; }
		}
		
		/// <summary>
		/// Whether or not to show the choices for the current paragraph.
		/// </summary>
		public bool ShowChoices {
			get { return m_showChoices; }
			set { m_showChoices = value; }
		}
		
		/// <summary>
		/// Sets the current passage to the passage with the specified title.
		/// </summary>
		/// <param name="passageTitle">
		/// The title of the passage to set as the current passage
		/// </param>
		public void SetCurrentPassage(string passageTitle) {
			m_currentPassage = m_story.GetPassage(passageTitle);
			m_currentLineIndex = 0;
			m_lastSpeechLineIndex = 0;
		}
	}
}
