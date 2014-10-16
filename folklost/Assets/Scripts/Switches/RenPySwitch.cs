using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the enabled state of a set of objects. Sets the enabled states to
/// true if all of the variable pairs evaluate to true.
/// 
/// If m_offscreenChange is enabled, then the objects will only change state
/// when the collider for the GameObject containing this switch is offscreen.
/// </summary>
public class RenPySwitch : MonoBehaviour {
	
	public VariablePair[] m_checks;
	public GameObject[] m_gameObjects;
	public bool m_offscreenChange = true;
	public Collider m_offscreenCollider;
	private bool colliding;
	
	// If true, moves the gameobject to a different position instead
	public bool m_doNotDisable = false;
	private Vector3[] m_originalPositions;
	
	void Start() {
		m_originalPositions = new Vector3[m_gameObjects.Length];
		for(int i = 0; i < m_gameObjects.Length; i++) {
			if(m_gameObjects[i] != null) {
				m_originalPositions[i] = m_gameObjects[i].transform.position;
			}
		}
		
		SetActiveState(CheckIfActive());
	}
	
	void Update () {
		bool active = CheckIfActive();
		
		bool seen = colliding;
		if(m_offscreenChange && !seen && Camera.main != null) {
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
			seen =  GeometryUtility.TestPlanesAABB(planes, m_offscreenCollider.bounds);
		}
		
		// Set the enabled state of all GameObjects
		if(!m_offscreenChange || (m_offscreenChange && !seen)) {
			SetActiveState(active);
		}
	}
	
	/// <summary>
	/// Checks if each variable pair is equal to its expected value.
	/// </summary>
	bool CheckIfActive() {
		bool active = true;
		foreach(VariablePair vp in m_checks) {
			string name = vp.key;
			string value = vp.value;
			if(!RenPy.Static.Variables.ContainsKey(name)) {
				if(value != "False") {
					active = false;
				}
			} else if(RenPy.Static.Variables[name] != value) {
				active = false;
			}
		}
		return active;
	}
	
	/// <summary>
	/// Sets the state of all of the objects to the specified state.
	/// </summary>
	void SetActiveState(bool active) {
		foreach(GameObject go in m_gameObjects) {
			if(go != null) {
				// Move the gameobject
				if(m_doNotDisable) {
					if(active) {
						for(int i = 0; i < m_gameObjects.Length; i++) {
							m_gameObjects[i].transform.position = m_originalPositions[i];
						}
					} else {
						for(int i = 0; i < m_gameObjects.Length; i++) {
							Vector3 newPos = m_originalPositions[i];
							newPos.y -= 100;
							m_gameObjects[i].transform.position = newPos;
						}
					}
				}
				// Enable/Disable the gameobject
				else {
					go.SetActive(active);
				}
			}
		}
	}
	
	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag == "Player") {
			StopAllCoroutines();
			this.colliding = true;
		}
	}
	
	void OnCollisionExit(Collision col) {
		if(col.gameObject.tag == "Player") {
			StartCoroutine(SetColliding(false));
		}
	}
	
	IEnumerator SetColliding(bool colliding) {
		yield return new WaitForSeconds(1);
		this.colliding = colliding;
	}
}
