using UnityEngine;
using System;
using System.Collections;

namespace Twine {
	
	[RequireComponent(typeof(AudioSource))]
	public class Dialog : MonoBehaviour
	{
		#region Properties
		
		/// <summary>
		/// The rollover sound
		/// </summary>
		public AudioClip m_rolloverSound;
		
		/// <summary>
		/// The selection sound
		/// </summary>
		public AudioClip m_selectSound;
		
		/// <summary>
		/// The texture used to draw black bars
		/// </summary>
		private Texture2D m_blackBars;
		
		private bool m_open;
		
		/// <summary>
		/// The twine source code
		/// </summary>
		public TextAsset m_twee;
		
		/// <summary>
		/// The state of the dialog
		/// </summary>
		private DialogState m_dialogState;
		
		/// <summary>
		/// Whether or not to show the dialog on Start
		/// </summary>
		public bool m_showDialog = false;
		
		/// <summary>
		/// Whether or not this dialog is open
		/// </summary>
		public bool Open {
			get { return m_open; }
		}
		
		private int hoverIndex;

		#endregion
		
		#region Unity Methods
		
		/// <summary>
		/// Parses a twine story on Awake
		/// </summary>
		void Awake () {
			// Setup the AudioSource
			GetComponent<AudioSource>().loop = false;
			
			// Parse the data
			TwineStory story = new TwineStory(m_twee);
			
			// Setup the state
			m_dialogState = new DialogState();
			m_dialogState.Dialog = this;
			m_dialogState.Story = story;
			
			m_blackBars = new Texture2D(1, 1, TextureFormat.RGBA32, false);
			m_blackBars.SetPixel(0, 0, new Color(0,0,0,.3f));
			m_blackBars.Apply();
		}
		
		void Start() {
			hoverIndex = -1;
			if(m_showDialog)
				StartDialog();
		}
		
		/// <summary>
		/// Renders the dialog to the screen
		/// </summary>
		void OnGUI() {
			if(!m_open) {
				return;
			}
			
			// Render black bars
			Rect position = new Rect();
			GUI.DrawTexture(position, m_blackBars, ScaleMode.StretchToFill);
			
			// Render the current line
			m_dialogState.LastSpeechLine.OnGUI();
			
			// Show the choices
			if(m_dialogState.ShowChoices) {
				GUIStyle style = new GUIStyle();
				style.alignment = TextAnchor.MiddleCenter;
				style.normal.textColor = Color.white;
				style.fontSize = 15;
				
				bool rolledOver = false;
				TwineLink[] links = m_dialogState.CurrentPassage.Links;
				Rect rect = new Rect(0, Screen.height-130, Screen.width, 30);
				for(int i = 0; i < links.Length; i++) {
					// Check if this choice is hidden
					if(!links[i].Show) {
						continue;
					}
					
					rect.y += 30;
					
					// Display the choice
					if(GUI.Button(rect, links[i].Text,style)) {
						
						AudioSource.PlayClipAtPoint(m_selectSound, Vector3.zero);
						
						// Set a variable, if needed
						if(links[i].SetsVariable) {
							string variable = links[i].Variable;
							string value = links[i].Value;
							if(Twine.Variables.ContainsKey(variable)) {
								Twine.Variables[variable] = value;
							} else {
								Twine.Variables.Add(variable, value);
							}
							
							if(Twine.DebugLines) {
								Debug.Log("Title: "+ m_dialogState.CurrentPassage.Title
								          + "  Line: " + m_dialogState.CurrentLineIndex
								          + "\nLink set variable $" + variable
								          + " to " + value + "\n\n");
							}
						}
						
						// Go to the selected passage
						m_dialogState.ShowChoices = false;
						m_dialogState.SetCurrentPassage(links[i].Target);
						NextLineCoroutine();
					}
					// Check if we rolled over the choice
					else if (rect.Contains(Event.current.mousePosition)) {
						rolledOver = true;
						if(i != hoverIndex) {
							hoverIndex = i;
							AudioSource.PlayClipAtPoint(m_rolloverSound, Vector3.zero);
						}
					}
				}
				
				if(!rolledOver) {
					hoverIndex = -1;
				}
			}
		}
		
		#endregion
		
		#region Public Control Methods
		
		/// <summary>
		/// Starts the dialog
		/// </summary>
		public void StartDialog() {
			m_open = true;
			m_dialogState.SetCurrentPassage("StartGame");
			NextLineCoroutine();
		}
		
		/// <summary>
		/// Stops the dialog
		/// </summary>
		public void StopDialog() {
			m_open = false;
			StopAllCoroutines();
		}

		public void NextLineCoroutine() {
			StartCoroutine(m_dialogState.CurrentLine.Execute(m_dialogState));
		}
		
		#endregion
	}
}
