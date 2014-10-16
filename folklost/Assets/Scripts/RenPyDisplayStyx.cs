using UnityEngine;
using System.Collections.Generic;
using RenPy;
using RenPy.Script;

public class RenPyDisplayStyx : RenPyDisplay
{
	public AudioClip m_rolloverAudio;
	public AudioClip m_selectAudio;
	public Texture m_selectTexture;
	public Font m_font;
	
	private bool m_enterPressed = false;
	private int m_selection = 0;
	private int m_maxIndex = 0;
	private Vector2 oldMousePos;
	
	private float m_alpha;
	private float Alpha {
		get { return Mathf.Abs(1-m_alpha); }
	}

	private PauseMenu m_pauseMenu;
	public PauseMenu PauseMenu {
		set { m_pauseMenu = value; }
	}
	
	protected override void RenPyUpdate(RenPyLineType mode) {
		// Don't bother doing anything if the game is paused
		if(m_pauseMenu.Open) {
			return;
		}
		
		switch(mode) {
			case RenPyLineType.SPEECH:
				// Check for input to go to next line
				if(Input.GetButtonDown("Interact") || Input.GetButtonDown("Enter")) {
					State.NextLine(this);
				}
				break;
			case RenPyLineType.MENU:
				int oldIndex = m_selection;
				if(Input.GetButtonDown("Up")) {
					m_selection--;
				}
				if(Input.GetButtonDown("Down")) {
					m_selection++;
				}
				if(Input.GetButtonDown("Enter")) {
					m_enterPressed = true;
				}
				m_selection = Mathf.Clamp(m_selection, 0, m_maxIndex-1);
				if(oldIndex != m_selection) {
					AudioSource.PlayClipAtPoint(m_rolloverAudio, transform.position);
				}
				break;
			case RenPyLineType.RETURN:
				// Stop the dialog
				if(mode == RenPyLineType.RETURN) {
					StopDialog();
				}
				break;
		}
		
		// Modulate the alpha value
		m_alpha += Time.deltaTime;
		if(m_alpha > 2) {
			m_alpha -= 2;
		}
	}
	
	protected override void RenPyOnGUI(RenPyLineType mode) {
		// Don't bother doing anything if the game is paused
		if(m_pauseMenu.Open) {
			return;
		}
		
		Rect rect = new Rect(0,0,0,0);
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.white;
		style.fontSize = UIStatic.FontSize;
		style.wordWrap = true;
		style.font = m_font;
		Texture2D blackBars = new Texture2D(1, 1, TextureFormat.RGBA32, false);
		blackBars.SetPixel(0, 0, new Color(0,0,0,.5f));
		blackBars.Apply();
		
		switch(mode) {
			case RenPyLineType.SPEECH:
				RenPySpeech speech = State.CurrentLine as RenPySpeech;
				if(speech == null) {
					Debug.LogError("Type mismatch!");
					State.NextLine(this);
				}
				
				// Reset selection variables;
				m_selection = 0;
				m_maxIndex = 0;
				m_enterPressed = false;
				
				// Calculate position
				rect = new Rect(0,0,0,0);
				rect.width = Mathf.Min(style.CalcSize(new GUIContent(speech.Text)).x + UIStatic.FontSize, Screen.width - UIStatic.FontSize*2);
				rect.height =  style.CalcHeight(new GUIContent(speech.Text), rect.width) + UIStatic.FontSize;
				rect.x = Screen.width/2 - rect.width/2;
				rect.y = Screen.height - rect.height - UIStatic.FontSize*3;
				
				// Draw the black background
				GUI.DrawTexture(rect, blackBars);
				
				// Render the speech
				GUI.Label(rect, speech.Text, style);
				break;
				
			case RenPyLineType.MENU:
				RenPyMenu menu = State.CurrentLine as RenPyMenu;
				if(menu == null) {
					Debug.LogError("Type mismatch!");
					State.NextLine(this);
				}
				
				// Display the choices
				rect = new Rect(0, Screen.height-130, Screen.width, 0);
				int index = 0;
				
				// Draw the black background
				float maxWidth = 0;
				float totalHeight = 0;
				int entries = menu.m_choices.Count;
				foreach(KeyValuePair<string, string> choice in menu.m_choices) {
					float wratio = ((float)m_selectTexture.width)/((float)m_selectTexture.height);
					float width  = style.CalcSize(new GUIContent(choice.Key)).x + UIStatic.FontSize;
					width += UIStatic.FontSize*wratio*3;
					maxWidth = Mathf.Max(maxWidth, width);
					totalHeight += UIStatic.FontSize * 1.5f;
				}
				
				rect = new Rect(Screen.width/2 - maxWidth/2, Screen.height - menu.m_choices.Count*UIStatic.FontSize*1.5f - 1.5f*UIStatic.FontSize, maxWidth, totalHeight + UIStatic.FontSize);
				GUI.DrawTexture(rect, blackBars);
				
				rect = new Rect(Screen.width, Screen.height - entries*UIStatic.FontSize*1.5f - UIStatic.FontSize, 0, 0);
				foreach(KeyValuePair<string, string> choice in menu.m_choices) {
					
					// Calculate position
					rect.width = style.CalcSize(new GUIContent(choice.Key)).x + UIStatic.FontSize;
					rect.height = style.CalcHeight(new GUIContent(choice.Key), rect.width);
					rect.x = Screen.width/2 - rect.width/2;
					
					// If this is the current selection, draw a few things
					if(m_selection == index) {
						float wratio = ((float)m_selectTexture.width)/((float)m_selectTexture.height);
						float hratio = 1f/wratio;
						Rect temp = new Rect(rect.x-(UIStatic.FontSize*wratio), rect.y+(rect.height-(UIStatic.FontSize*hratio))/2, UIStatic.FontSize*wratio, UIStatic.FontSize*hratio);
						Color c = Color.white;
						c.a = Alpha;
						GUI.color = c;
						GUI.DrawTexture(temp, m_selectTexture);
						GUI.color = Color.white;
					}
					
					// Check if a choice was selected
					if(GUI.Button(rect, choice.Key, style) || (m_selection == index && m_enterPressed)) {
						State.GoToLabel(this, choice.Value);
						AudioSource.PlayClipAtPoint(m_selectAudio, transform.position);
					}
					// Check if the choice was rolled over
					else if(rect.Contains(Event.current.mousePosition) && oldMousePos != Event.current.mousePosition) {
						if(m_selection != index) {
							m_selection = index;
							AudioSource.PlayClipAtPoint(m_rolloverAudio, transform.position);
						}
						oldMousePos = Event.current.mousePosition;
					}
					
					index++;
					rect.y += UIStatic.FontSize * 1.5f;
				}
				m_maxIndex = index;
				
				break;
				
			default:
				// Show nothing for this line, proceed to the next one.
				State.NextLine(this);
				break;
		}
	}
}
