using UnityEngine;
using System.Collections;

/// <summary>
/// Attempts to hide and unhide an object when it is not seen by the player
/// </summary>
public class Disappear : MonoBehaviour {
	
	public Camera[] m_cameras;
	public Collider[] m_colliders;
	
	public MonoBehaviour[] m_behaviours;
	public OffscreenHider[] m_offscreenHiders;
	public Renderer[] m_renderers;
	public GameObject[] m_gameObjects;
	
	/// <summary>
	/// Whether or not to enable or disable the renderers and MonoBehaviours
	/// </summary>
	public bool m_setEnabledTo;
	
	void Update() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Bounds playerBounds = player.collider.bounds;
		
		bool seen = false;
		foreach(Collider collider in m_colliders) {
			if(seen) {
				break;
			}
			seen = collider.bounds.Contains(player.transform.position)
			        || collider.bounds.Intersects(playerBounds);
		}
		foreach(Camera camera in m_cameras) {
			if(seen) {
				break;
			}
			
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
			
			foreach(Collider collider in m_colliders) {
				seen = GeometryUtility.TestPlanesAABB(planes,collider.bounds);
				if(seen) {
					break;
				}
			}
		}
		
		if(!seen) {
			foreach(MonoBehaviour behaviour in m_behaviours) {
				behaviour.enabled = m_setEnabledTo;
			}
			foreach(Renderer renderer in m_renderers) {
				renderer.enabled = m_setEnabledTo;
			}
			foreach(GameObject gameObject in m_gameObjects) {
				gameObject.SetActive(m_setEnabledTo);
			}
			foreach(OffscreenHider oh in m_offscreenHiders) {
				oh.m_hide = m_setEnabledTo;
			}
		}
	}
}
