using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {

	public FootstepsMeta defaultFootsteps;
	private FootstepsMeta meta;
	private AudioSource source;

	public void Awake() {
		source = gameObject.AddComponent<AudioSource>();
		source.loop = false;
		source.playOnAwake = false;
	}

	void Start() {
		InvokeRepeating ("PlaySound", 0.0f, .60f);
	}

	void PlaySound() {
		if(Input.GetButton("Vertical") || Input.GetButton ("Horizontal")) {
			if(meta == null) {
				defaultFootsteps.PlayFootstep(source);
			} else {
				meta.PlayFootstep(source);
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		FootstepsMeta fm = col.transform.gameObject.GetComponent<FootstepsMeta>();
		if(fm != null) {
			meta = fm;
		}
	}

	void OnTriggerExit(Collider col) {
		FootstepsMeta fm = col.transform.gameObject.GetComponent<FootstepsMeta>();
		if(fm != null && fm == meta) {
			meta = null;
		}
	}

	void OnCollisionEnter(Collision col) {
		FootstepsMeta fm = col.transform.gameObject.GetComponent<FootstepsMeta>();
		if(fm != null) {
			meta = fm;
		}
	}

	void OnCollisionExit(Collision col) {
		FootstepsMeta fm = col.transform.gameObject.GetComponent<FootstepsMeta>();
		if(fm != null && fm == meta) {
			meta = null;
		}
	}
}
