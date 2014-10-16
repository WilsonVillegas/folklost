using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	/// <summary>
	/// The rigidbody that this player controller uses to move
	/// </summary>
	public Rigidbody m_rigidbody;
	
	/// <summary>
	/// The Head Bob controller
	/// </summary>
	public HeadBobbingController m_headbob;
	
	/// <summary>
	/// The Crosshair script
	/// </summary>
	public Crosshair m_crosshair;
	
	/// <summary>
	/// The pause menu
	/// </summary>
	public PauseMenu m_pauseMenu;
	
	/// <summary>
	/// The direction the player is moving in local space
	/// </summary>
	private Vector2 m_motion;
	
	/// <summary>
	/// The angular velocity of the player's camera movement
	/// </summary>
	private Vector2 m_angularVelocity;
	
	/// <summary>
	/// The angle of the player's camera
	/// </summary>
	private Vector2 m_angle;
	
	/// <summary>
	/// The speed at which the player can move
	/// </summary>
	private float m_speed = 4.0f;
	public float Speed {
		get { return RenPy.Static.Run ? 14.0f : m_speed; }
	}
	
	/// <summary>
	/// Whether or not the player's look controls are inverted
	/// </summary>
	private bool m_inverted = false;
	public bool Inverted {
		get { return m_inverted; }
		set { m_inverted = value; }
	}
	
	/// <summary>
	/// The sensitivity of the player's camera movement
	/// </summary>
	private float m_sensitivity = 300.0f;
	public float Sensitivity {
		get { return m_sensitivity; }
		set { m_sensitivity = value; }
	}
	
	public bool Moving {
		get { return m_motion.x != 0 || m_motion.y != 0; }
	}
	
	private Directional m_dir;
	private int m_xdir, m_zdir;
	
	void OnGUI() {
		// Parse key down events
		if(Event.current.type == EventType.KeyDown) {
			switch(Event.current.keyCode) {
				case KeyCode.A: m_dir.left = 1; m_xdir = 1; break;
				case KeyCode.D: m_dir.right = 1; m_xdir = -1; break;
				case KeyCode.W: m_dir.forward = 1; m_zdir = -1; break;
				case KeyCode.S: m_dir.backwards = 1; m_zdir = 1; break;
				default: break;
			}
		}
		// Parse key up events
		else if(Event.current.type == EventType.KeyUp) {
			switch(Event.current.keyCode) {
				case KeyCode.A: m_dir.left = 0; m_xdir = 0; break;
				case KeyCode.D: m_dir.right = 0; m_xdir = 0; break;
				case KeyCode.W: m_dir.forward = 0; m_zdir = 0; break;
				case KeyCode.S: m_dir.backwards = 0; m_zdir = 0; break;
				default: break;
			}
		}
		
		// Reset the motion vector
		m_motion.x = 0.0f;
		m_motion.y = 0.0f;
		
		// Calculate movement on the x and z plane
		Vector2 hmove = new Vector2();
		hmove.x = m_xdir != 0 ? m_xdir : m_dir.left - m_dir.right;
		hmove.y = m_zdir != 0 ? m_zdir : m_dir.backwards - m_dir.forward;
		
		if(hmove.x != 0 || hmove.y != 0) {
			hmove.Normalize();
			m_motion.x = hmove.x;
			m_motion.y = hmove.y;
		}
	}
	
	void OnEnable() {
		SaveTransformRotation();
	}
	
	void Update() {
		// Check if we should render the crosshair
		m_crosshair.enabled = !m_pauseMenu.Open;
		// Parse mouse movement
		if(Screen.lockCursor) {
			m_angularVelocity.x += Input.GetAxisRaw("Mouse X") * m_sensitivity;
			m_angularVelocity.y += (m_inverted ? -1 : 1) * Input.GetAxisRaw("Mouse Y") * m_sensitivity;
		} else {
			ResetMovement();
		}
		// Mouse sensitivity shortcuts
		if(Input.GetKeyDown(KeyCode.LeftBracket)) {
			if(m_sensitivity > 20) {
				m_sensitivity -= 20.0f;
				ToastMachine notif = GameObject.Find("ToastMachine").GetComponent<ToastMachine>();
				notif.Toast("Mouse sensitivity set to " + (int) m_sensitivity);
			}
		}
		if(Input.GetKeyDown(KeyCode.RightBracket)) {
			if(m_sensitivity < 1000) {
				m_sensitivity += 20.0f;
				ToastMachine notif = GameObject.Find("ToastMachine").GetComponent<ToastMachine>();
				notif.Toast("Mouse sensitivity set to " + (int) m_sensitivity);
			}
		}
		if(Input.GetKeyDown(KeyCode.Backslash)) {
			if(m_sensitivity < 1000) {
				m_inverted = !m_inverted;
				ToastMachine notif = GameObject.Find("ToastMachine").GetComponent<ToastMachine>();
				if(m_inverted) {
					notif.Toast("Mouse set to be inverted");
				} else {
					notif.Toast("Mouse set to not be inverted");
				}
			}
		}
	}
	
	void FixedUpdate() {
		// Calculate the camera angle
		m_angle += m_angularVelocity * Time.fixedDeltaTime;
		m_angularVelocity = new Vector2();
		
		m_angle.x = Wrap(m_angle.x, 0.0f, 360.0f);
		m_angle.y = Mathf.Clamp(NormalizeY(m_angle.y), -89.99f, 89.99f);
		
		Vector3 dir = SphericalToRect(new Vector3(Mathf.Deg2Rad * m_angle.x, Mathf.Deg2Rad * m_angle.y, 1.0f));
		Vector3 right = Vector3.Cross(dir, new Vector3(0.0f, 1.0f, 0.0f)).normalized;
		Vector3 up = Vector3.Cross(right, dir).normalized;
		
		// Set angle of camera
		transform.LookAt(transform.position + dir, up);
		
		// Calculate the velocity of the rigidbody
		Vector3 m_velocity = new Vector3(0,0,0);
		m_velocity += -dir * m_motion.y * Speed;
		m_velocity += right * m_motion.x * Speed;
		m_velocity.y = m_rigidbody.velocity.y;
		
		// Set velocity of rigidbody
		m_rigidbody.velocity = m_velocity;
	}
	
	void OnDisable() {
		SaveTransformRotation();
		ResetMovement();
	}
	
	public void LookAt(Transform target) {
		transform.LookAt(target);
		SaveTransformRotation();
	}
	
	public void LookAt(Transform target, Vector3 worldUp) {
		transform.LookAt(target, worldUp);
		SaveTransformRotation();
	}
	
	public void LookAt(Vector3 worldPosition) {
		transform.LookAt(worldPosition);
		SaveTransformRotation();
	}
	
	public void LookAt(Vector3 worldPosition, Vector3 worldUp) {
		transform.LookAt(worldPosition, worldUp);
		SaveTransformRotation();
	}
	
	public void SmoothLookAt(Vector3 worldPosition) {
		Quaternion from = transform.rotation;
		
		transform.LookAt(worldPosition);
		Quaternion to = transform.rotation;
		
		StopAllCoroutines();
		StartCoroutine(SmoothLookAt(from, to));
	}
	
	private IEnumerator SmoothLookAt(Quaternion from, Quaternion to) {
		float ratio = 0;
		while(ratio < 1) {
			transform.rotation = Quaternion.Slerp(from, to, ratio);
			yield return new WaitForEndOfFrame();
			ratio += Time.deltaTime;
		}
		transform.rotation = to;
		SaveTransformRotation();
	}
	
	public void SaveTransformRotation() {
		m_angle.x = transform.rotation.eulerAngles.y;
		m_angle.y = -transform.rotation.eulerAngles.x;
	}
	
	private void ResetMovement() {
		m_dir.left = 0;
		m_dir.right = 0;
		m_dir.forward = 0;
		m_dir.backwards = 0;
		m_zdir = 0;
		m_xdir = 0;
	}
	
	private float NormalizeY(float angle) {
		while(angle < -180) {
			angle += 360.0f;
		}
		while(angle > 180) {
			angle -= 360.0f;
		}
		return angle;
	}

	private float Wrap(float value, float min, float max) {
		while(value < min) {
			value += max - min;
		}
		while(value > max) {
			value -= max - min;
		}
		return value;
	}
	
	private Vector3 SphericalToRect(Vector3 spherical)
	{
		Vector3 ret = new Vector3(0,0,0);
		ret.x = Mathf.Cos(spherical.y) * Mathf.Sin(spherical.x);
		ret.y = Mathf.Sin(spherical.y);
		ret.z = Mathf.Cos(spherical.y) * Mathf.Cos(spherical.x);
		return ret;
	}
	
	public void LockControllers() {
		this.enabled = false;
		m_headbob.enabled = false;
		m_crosshair.enabled = false;
	}
	
	public void ReleaseControllers() {
		this.enabled = true;
		m_headbob.enabled = true;
		m_crosshair.enabled = true;
	}
}

struct Directional {
	public int left;
	public int right;
	public int forward;
	public int backwards;
}