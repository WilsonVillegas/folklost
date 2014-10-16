using UnityEngine;
using System.Collections;

public class HideOnAwake : MonoBehaviour {

	void Awake() {
		renderer.enabled = false;
	}
}
