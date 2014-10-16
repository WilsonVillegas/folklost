using UnityEngine;
using System.Collections.Generic;
using RenPy.Parser;
using RenPy.Script;

namespace RenPy
{
	public class RenPyDisplayBasic : RenPyDisplay
	{
		protected override void RenPyUpdate(RenPyLineType mode) {
			switch(mode) {
				case RenPyLineType.SPEECH:
					// Check for input to go to next line
					if(Input.GetButtonDown("Interact")) {
						State.NextLine(this);
					}
					break;
				case RenPyLineType.RETURN:
					// Stop the dialog
					if(mode == RenPyLineType.RETURN) {
						StopDialog();
					}
					break;
			}
		}
		
		protected override void RenPyOnGUI(RenPyLineType mode) {
			Rect rect;
			GUIStyle style = new GUIStyle();
			style.alignment = TextAnchor.MiddleCenter;
			style.normal.textColor = Color.white;
			style.fontSize = 15;
			style.wordWrap = true;
			
			switch(mode) {
				case RenPyLineType.SPEECH:
					RenPySpeech speech = State.CurrentLine as RenPySpeech;
					if(speech == null) {
						Debug.LogError("Type mismatch!");
						State.NextLine(this);
					}
					
					// Render the speech
					rect = new Rect(50, Screen.height-250, Screen.width-100, 100);
					GUI.Label(rect, speech.Text, style);
					break;
					
				case RenPyLineType.MENU:
					RenPyMenu menu = State.CurrentLine as RenPyMenu;
					if(menu == null) {
						Debug.LogError("Type mismatch!");
						State.NextLine(this);
					}
					
					// Display the choices
					rect = new Rect(0, Screen.height-130, Screen.width, 30);
					foreach(KeyValuePair<string, string> choice in menu.m_choices) {
						
						// Check if a choice was selected
						if(GUI.Button(rect, choice.Key, style)) {
							State.GoToLabel(this, choice.Value);
						}
						rect.y += 30;
					}
					break;
					
				default:
					// Show nothing for this line, proceed to the next one.
					State.NextLine(this);
					break;
			}
		}
	}
}
