using UnityEngine;
using System.Collections;

public class RotationBehaviour : MonoBehaviour {
	
	public Vector3 m_rotationAxis;
	public float m_speed;
	public Space m_relativeTo;
	
	void Update() {
		transform.Rotate(m_rotationAxis, m_speed * Time.deltaTime, m_relativeTo);
	}
}
