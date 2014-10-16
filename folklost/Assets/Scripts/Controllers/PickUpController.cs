using UnityEngine;
using System.Collections;
using RenPy;

public class PickUpController : MonoBehaviour {
	
	public PlayerController m_controller;
	public Crosshair crosshair;
	public Font m_font;
	
	/// <summary>
	/// The object we are currently holding
	/// </summary>
	private GameObject holding;
	
	/// <summary>
	/// The current Narration
	/// </summary>
	private RenPyDisplayStyx m_renpyDialog;
	
	/// <summary>
	/// The last interactable object
	/// </summary>
	private Interactable m_lastInteractable;
	
	private float raycastDistance = 3f;
	private string hoverText;
	
	void Update () {
		// Don't bother doing anything if the game is paused
		if(m_controller.m_pauseMenu.Open) {
			return;
		}

		hoverText = null;
		RaycastHit hit;
		bool interact = Input.GetButtonDown("Interact");
		bool raycast = Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance);
		
		// Check if a dialog is still open
		if(m_renpyDialog != null) {
			if(m_renpyDialog.Open == false) {
				m_controller.ReleaseControllers();
				m_renpyDialog = null;
			} else {
				return;
			}
			Screen.lockCursor = false;
		} else {
			Screen.lockCursor = true;
		}
		
		if(interact) {
			// Drop the object we are holding
			if(holding != null) {
				DropObject();
			} else if(raycast) {
				GameObject hitObj = hit.collider.gameObject;
				
				// Interact with an object
				Interactable inter = hitObj.GetComponent<Interactable>();
				if(inter != null) {
					inter.Interact();
				}
				
				m_renpyDialog = hitObj.GetComponentInChildren<RenPyDisplayStyx>();
				if(m_renpyDialog != null && hitObj.tag == "dialog") {
					// Start the dialog and lock the controllers
					m_renpyDialog.PauseMenu = m_controller.m_pauseMenu;
					m_renpyDialog.StartDialog();
					m_controller.LockControllers();
					m_controller.SmoothLookAt(hitObj.transform.position);
					Screen.lockCursor = false;
				}
				
				// Pick up an object
				if(hitObj.tag == "pickup") {
					PickUpObject(hitObj);
				}
			}
		}
		
		// See if we are hovering over an interactable object
		if (holding == null) {
			if(raycast) {
				crosshair.SetCrosshair(Crosshair.Type.NORMAL);
				
				Interactable inter = hit.collider.gameObject.GetComponent<Interactable>();
				if(m_lastInteractable != inter) {
					if(m_lastInteractable != null)
						m_lastInteractable.OnHoverExit();
					if(inter != null)
						inter.OnHoverEnter();
					m_lastInteractable = inter;
				}
				if(inter != null) {
					inter.OnHover();
					hoverText = inter.GetHoverText();
					crosshair.SetCrosshair(Crosshair.Type.INSPECT);
				}
				
				RenPyDisplayStyx renpy = hit.collider.gameObject.GetComponentInChildren<RenPyDisplayStyx>();
				if(renpy != null && hit.collider.gameObject.tag == "dialog") {
					crosshair.SetCrosshair(Crosshair.Type.TALK);
				}
				
				string tag = hit.collider.gameObject.tag;
				if(tag == "pickup" || tag == "item") {
					crosshair.SetCrosshair(Crosshair.Type.PICKUP);
				}
			} else {
				crosshair.SetCrosshair(Crosshair.Type.NORMAL);
				
				if(m_lastInteractable != null)
					m_lastInteractable.OnHoverExit();
				m_lastInteractable = null;
			}
		}
		// Move the object we are holding
		else {
			float distance = raycastDistance-1;
			if(raycast) {
				distance = Mathf.Min(distance, hit.distance-1);
				distance = Mathf.Max(1, distance);
			}
			Ray rai = new Ray(transform.position, transform.forward);
			Vector3 pos = rai.GetPoint(distance);
			holding.transform.position = pos;
		}
	}
	
	private void PickUpObject(GameObject go) {
		if(holding != null) {
			return;
		}
		
		holding = go;
		holding.transform.parent = transform;
		holding.collider.attachedRigidbody.detectCollisions = false;
		holding.collider.attachedRigidbody.useGravity = false;
		holding.collider.enabled = false;
	}
	
	private void DropObject() {
		if(holding == null) {
			return;
		}
		
		holding.transform.parent = null;
		holding.collider.enabled = true;
		holding.collider.attachedRigidbody.useGravity = true;
		holding.collider.attachedRigidbody.detectCollisions = true;
		holding = null;
	}
	
	void OnGUI() {
		// Don't bother doing anything if the game is paused
		if(m_controller.m_pauseMenu.Open) {
			return;
		}
		
		if(hoverText == null) {
			return;
		}
		
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.white;
		style.fontSize = UIStatic.FontSize;
		style.wordWrap = true;
		style.font = m_font;
		
		Rect rect = new Rect(0,0,0,0);
		rect.width = style.CalcSize(new GUIContent(hoverText)).x + UIStatic.FontSize;
		rect.height =  style.CalcHeight(new GUIContent(hoverText), rect.width) + UIStatic.FontSize;
		rect.x = Screen.width/2 - rect.width/2;
		rect.y = Screen.height - 200;
		
		Texture2D blackBars = new Texture2D(1, 1, TextureFormat.RGBA32, false);
		blackBars.SetPixel(0, 0, new Color(0,0,0,.5f));
		blackBars.Apply();
		
		// Render the black background
		GUI.DrawTexture(rect, blackBars);
		
		// Render the speech
		GUI.Label(rect, hoverText, style);
	}
}
