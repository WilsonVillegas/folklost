using UnityEngine;
using System;
using System.Collections;
using Twine.Util;

namespace Twine {
	
	/// <summary>
	/// Represents a line of speech in Twine.
	/// </summary>
	public class TwineSpeech : TwineLine
	{
		#region Properties
		
		private string m_text;
		private float m_time;
		private string m_voiceOverFilename;
		
		/// <summary>
		/// The text for this line
		/// </summary>
		public string Text {
			get { return m_text; }
		}
		
		/// <summary>
		/// The amount of time this line takes
		/// </summary>
		public float Time {
			get { return m_time; }
		}
		
		/// <summary>
		/// The name of the voice over file for this line
		/// </summary>
		public string VoiceOverFilename {
			get { return m_voiceOverFilename; }
		}
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Parses a line from the two strings given to it.
		/// </summary>
		/// <param name="metadata">The metadata string</param>
		/// <param name="text">The line</param>
		public TwineSpeech(ref Scanner scan) {
			// Default metadata values
			m_time = -1;
			
			string mode = scan.Peek();
			
			if(mode == "{{") {
				scan.Next(); // Should be {{
				while(scan.HasNext()) {
					string token = scan.Next();
					if(token == "}}") {
						break;
					} else if(token == ",") {
						continue;
					} else if(token == "time") {
						scan.Next(); // Should be a colon
						m_time = float.Parse(scan.Next(), System.Globalization.NumberStyles.Any);
					} else if(token == "voiceover") {
						scan.Next(); // Should be a colon
						m_voiceOverFilename = scan.Next();
					}
				}
				scan.Next(); // Should be '\n'
			}
			
			while(scan.HasNext()) {
				string token = scan.Next();
				
				if(token == "\n") {
					break;
				}
				
				if(m_text == null) {
					m_text = token;
				} else {
					m_text += " " + token;
				}
			}
		}
		
		#endregion
		
		#region Methods
		
		public override IEnumerator Execute(DialogState state) {
			// Set this line as the last speech line
			state.LastSpeechLineIndex = state.CurrentLineIndex;
			
			// Play voiceover
			string voiceover = "VoiceOver/" + m_voiceOverFilename;
			state.Dialog.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(voiceover);
			state.Dialog.GetComponent<AudioSource>().Play();
			
			// Print debugging, if needed
			if(Twine.DebugLines) {
				Debug.Log("Title: "+ state.CurrentPassage.Title
				           + "  Line: " + state.CurrentLineIndex
				           + "  Time: " + m_time
				           + "  Voiceover: " + (m_voiceOverFilename ?? "none")
				           + "\nText: " + m_text
				           + "\n\n");
			}
			
			// Wait
			if(m_time > 0 && !Twine.SkipDialog) {
				yield return new WaitForSeconds(m_time);
			}
			
			// Go to the next line
			state.CurrentLineIndex++;
			if(state.CurrentLineIndex < state.CurrentPassage.Lines.Length) {
				state.Dialog.NextLineCoroutine();
			}
			
			// If there is no next line, show the choices
			else {
				state.ShowChoices = true;
				
				// If there are no choices, then we have reached the end of the passage.
				if(state.CurrentPassage.Links.Length <= 0) {
					state.Dialog.StopDialog();
				}
			}
		}

		public override void OnGUI() {
			GUIStyle style = new GUIStyle();
			style.alignment = TextAnchor.MiddleCenter;
			style.normal.textColor = Color.white;
			style.fontSize = 15;
			style.wordWrap = true;
			Rect rect = GUILayoutUtility.GetRect(new GUIContent(m_text), style);
			// Add Padding
			rect.width += 40;
			rect.height += 5;
			// Move to correct position
			rect.x = Screen.width/2 - rect.width/2;
			rect.y = Screen.height - 200;
			
			Texture2D blackBars = new Texture2D(1, 1, TextureFormat.RGBA32, false);
			blackBars.SetPixel(0, 0, new Color(0,0,0,.5f));
			blackBars.Apply();
			GUI.DrawTexture(rect,blackBars);
			
			GUI.Label(rect, m_text, style);
		}
		
		#endregion
	}
}
