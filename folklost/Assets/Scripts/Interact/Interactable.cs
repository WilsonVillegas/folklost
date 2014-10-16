using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {
	
	/// <summary>
	/// Called when the player tries to interact with this object
	/// </summary>
	public abstract void Interact();
	
	/// <summary>
	/// Called when the player starts hovering over this object
	/// </summary>
	public abstract void OnHoverEnter();
	
	/// <summary>
	/// Called when the player is hovering over this object
	/// </summary>
	public abstract void OnHover();
	
	/// <summary>
	/// Called when the player is hovering over this object
	/// </summary>
	public abstract void OnHoverExit();
	
	/// <summary>
	/// Returns the text to display for this object when hovered over.
	/// </summary>
	/// <returns>the text to display for this object</returns>
	public abstract string GetHoverText();
}
