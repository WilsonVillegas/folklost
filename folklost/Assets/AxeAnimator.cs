using UnityEngine;
using System.Collections;

public class AxeAnimator : MonoBehaviour {

	private bool swinging = false;
	private int index = 0;
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(!swinging && index > 0)
				StartCoroutine(Swing());
			index++;
		}
	}

	private IEnumerator Swing()
	{
		swinging = true;
		animation.Play();
		yield return new WaitForSeconds(1f);
		swinging = false;
	}
}
