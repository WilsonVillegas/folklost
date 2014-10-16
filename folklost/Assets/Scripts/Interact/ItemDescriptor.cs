using UnityEngine;
using System.Collections.Generic;
using RenPy;
using RenPy.Script;

public class ItemDescriptor : Interactable {
	
	public string m_itemName;
	//public string m_variableName;
	public string m_description;
	//public Material m_outlineMaterial;
	
	//public AudioClip m_pickupSound;
	
	public override void Interact() {
		// Play the pickup sound
		//AudioSource.PlayClipAtPoint(m_pickupSound, this.transform.position);
		
		// Set the specified variable to true
		Static.LogAction("Inspecting" + m_itemName);
	}
	public override void OnHoverEnter() {
		// Nothing to do);
	}
	
	public override void OnHover() {
		// Nothing to do
	}
	
	public override void OnHoverExit() {
		// Nothing to do
	}
	
	public void OnDestroy() {
		// Nothing to do
	}
	
	public override string GetHoverText() {
		//return "Inspect " + (m_itemName ?? m_variableName);
		
		return m_itemName + "\n" + m_description;
	}
//	
//	public void SetAlpha(float a) {
//		if(m_outlineMaterial == null) {
//			return;
//		}
//		
//		Color c = m_outlineMaterial.GetColor("_OutlineColor");
//		c.a = a;
//		m_outlineMaterial.SetColor("_OutlineColor", c);
//	}
}
