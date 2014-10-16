using UnityEngine;
using System.Collections;
using RenPy;

public class ItemInteractable : Interactable {
	
	public string m_itemName;
	public string m_variableName;
	public Material m_outlineMaterial;
	public bool m_destroyOnPickup = true;
	
	public AudioClip m_pickupSound;
	
	public override void Interact() {
		// Play the pickup sound
		AudioSource.PlayClipAtPoint(m_pickupSound, this.transform.position);
		
		// Set the specified variable to true
		Static.LogAction("Setting " + m_variableName + " to True");
		Static.Variables[m_variableName] = "True";
		
		// Push a notification to the toast machine
		if(m_itemName != null) {
			ToastMachine.Instance.Toast("Picked up " + m_itemName);
		} else {
			ToastMachine.Instance.Toast("Picked up " + m_variableName);
		}
		
		// Destroy this GameObject
		if(m_destroyOnPickup)
			Destroy(this.gameObject);
	}
	
	public override void OnHoverEnter() {
		SetAlpha(1.0f);
	}
	
	public override void OnHover() {
		// Nothing to do
	}
	
	public override void OnHoverExit() {
		SetAlpha(0.0f);
	}
	
	public void OnDestroy() {
		SetAlpha(0.0f);
	}
	
	public override string GetHoverText() {
		return "Pick up " + (m_itemName ?? m_variableName);
	}
	
	public void SetAlpha(float a) {
		if(m_outlineMaterial == null) {
			return;
		}
		
		Color c = m_outlineMaterial.GetColor("_OutlineColor");
		c.a = a;
		m_outlineMaterial.SetColor("_OutlineColor", c);
	}
}
