using UnityEngine;
using System.Collections;

public class LiftInteractable : Interactable {
	
	public LiftController m_lift;
	public bool up;
	
	public override void Interact() {
		if(!m_lift.Moving) {
			m_lift.Move(up);
		}
	}
	
	public override void OnHoverEnter() {
		// Nothing to do
	}
	
	public override void OnHover() {
		// Nothing to do
	}
	
	public override void OnHoverExit() {
		// Nothing to do
	}
	
	public override string GetHoverText()
	{
		return m_lift.Moving ? null : (up ? "Go Up" : "Go Down");
	}
}
