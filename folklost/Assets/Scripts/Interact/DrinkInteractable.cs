using UnityEngine;
using System.Collections;
using RenPy;

public class DrinkInteractable : Interactable {
	
	public string m_itemName;
	public string m_description;
	public string m_variableName;
	public Material m_outlineMaterial;
	public bool m_destroyOnPickup = true;
	
	public AudioClip m_pickupSound;
	
	private static int num_drinks = 0;
	private const int MAX_DRINKS = 2;
	private string[] m_drinkVarNames = { "item_gin", "item_tonic", "item_vodka", "item_orange_juice", "item_cola", "item rum" };

	public override void Interact() {
		// Don't pick this up if we have already picked up max drinks
		if(num_drinks >= MAX_DRINKS) {
			return;
		}
		
		// Increase the number of drinks
		num_drinks++;
		
		// Play the pickup sound
		AudioSource.PlayClipAtPoint(m_pickupSound, this.transform.position);
		
		// Set the specified variable to true
		string drink = GetHeldDrink();
		Static.LogAction("Setting " + m_variableName + " to True");
		Static.Variables[m_variableName] = "True";
		
		// Push a notification to the toast machine
		if(m_itemName != null) {
			if(num_drinks > 1) {
				ToastMachine.Instance.Toast("Mixed " + drink + " with " + m_itemName);
			} else {
				ToastMachine.Instance.Toast("Picked up " + m_itemName);
			}
		} else {
			if(num_drinks > 1) {
				ToastMachine.Instance.Toast("Mixed " + drink + " with " + m_variableName);
			} else {
				ToastMachine.Instance.Toast("Picked up " + m_variableName);
			}
		}
		
		// Destroy this GameObject
		if(m_destroyOnPickup)
			Destroy(this.gameObject);
	}
	
	public override void OnHoverEnter() {
		// If all of the variables are false, then the number of drinks should be reset to 0
		bool reset = true;
		foreach(string varName in m_drinkVarNames) {
			if(Static.Variables.ContainsKey(varName) && Static.Variables[varName] == "True") {
				reset = false;
				break;
			}
		}
		if(reset) {
			num_drinks = 0;
		}
		
		if(num_drinks < MAX_DRINKS)
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
		if(num_drinks >= MAX_DRINKS)
			return null;
		if(num_drinks > 0)
			return "Mix " + GetHeldDrink() + " with " + m_description;
		return "Pick up " + m_description;
		//return "Pick up " + (m_itemName ?? m_variableName);
	}
	
	public void SetAlpha(float a) {
		if(m_outlineMaterial == null) {
			return;
		}
		
		Color c = m_outlineMaterial.GetColor("_OutlineColor");
		c.a = a;
		m_outlineMaterial.SetColor("_OutlineColor", c);
	}

	public string GetHeldDrink() {
		foreach(string varName in m_drinkVarNames) {
			if(Static.Variables.ContainsKey(varName) && Static.Variables[varName] == "True") {
				string[] arr = varName.Split(new char[] {'_'});
				string name = "";
				for(int i = 1; i < arr.Length; i++) {
					if(name != "") {
						name += " ";
					}
					name += char.ToUpper(arr[i][0]) + arr[i].Substring(1);
				}
				return name;
			}
		}
		return null;
	}
}
