using UnityEngine;
using System.Collections;

public class HeadBobbingController : MonoBehaviour {
	
	private float timer = 0.0f;
	private float bobbingSpeed = 7f;
	private float bobbingAmount = 0.05f;
	private float midpoint;
	
	void Awake() {
		midpoint = transform.localPosition.y;
	}
	
	void Update () {
		float waveslice = 0.0f;
		
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		if(!Screen.lockCursor) {
			horizontal = 0;
			vertical = 0;
		}
		
		if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) {
			timer = 0.0f;
		} else {
			waveslice = Mathf.Sin(timer);
			timer = timer + bobbingSpeed * Time.deltaTime;
			if (timer > Mathf.PI * 2) {
				timer = timer - (Mathf.PI * 2);
			}
		}
		
		if (waveslice != 0) {
			float translateChange = waveslice * bobbingAmount;
			float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
			totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
			translateChange = totalAxes * translateChange;
			
			// Set the local position
			Vector3 pos = transform.localPosition;
			pos.y = midpoint + translateChange;
			transform.localPosition = pos;
		}
		else {
			// Reset the local position
			Vector3 pos = transform.localPosition;
			pos.y = midpoint;
			transform.localPosition = pos;
		}
	}
}
