using UnityEngine;
using System.Collections;

public class LiftController : MonoBehaviour {
	
	public Animation m_animation;
	public AudioClip m_openSound;
	public AudioClip m_closeSound;
	public AudioClip m_liftSound;
	public CameraShakeController m_cameraShake;
	public Collider m_doorCollider;
	public Transform m_playerTransform;
	public Transform[] m_locations;
	public int m_startIndex;
	public float m_time;
	
	private int m_index;
	
	private bool m_moving;
	public bool Moving {
		get { return m_moving; }
	}
	
	void Start() {
		m_index = m_startIndex;
	}
	
	public void Move(bool up) {
		if(!CanMove(up)) {
			return;
		}
		
		m_index += up ? 1 : -1;
		StartCoroutine(Move());
	}
	
	private IEnumerator Move() {
		m_moving = true;
		m_doorCollider.enabled = true;
		
		AudioSource.PlayClipAtPoint(m_openSound, this.transform.position);
		AudioSource.PlayClipAtPoint(m_liftSound, this.transform.position);
		m_animation.Play("Close");
		yield return new WaitForSeconds(m_animation["Close"].length);
		
		// "Move" the elevator
		m_cameraShake.Shake(m_time - 3f, 0.015f);
		
		// Magically teleport
		Vector3 from = transform.position;
		Vector3 to = m_locations[m_index].position;
		Vector3 diff = to - from;
		transform.position = to;
		m_playerTransform.position += diff;
		
		// Open the lift
		yield return new WaitForSeconds(m_time);
		AudioSource.PlayClipAtPoint(m_closeSound, this.transform.position);
		m_animation.Play("Open");
		yield return new WaitForSeconds(m_animation["Open"].length);
		
		m_doorCollider.enabled = false;
		m_moving = false;
	}
	
	public bool CanMove(bool up) {
		int newIndex = m_index + (up ? 1 : -1);
		if(newIndex >= 0 && newIndex < m_locations.Length) {
			return true;
		}
		return false;
	}
}
