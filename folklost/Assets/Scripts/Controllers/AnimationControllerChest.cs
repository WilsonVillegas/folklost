using UnityEngine;
using System.Collections;
using RenPy;

public class AnimationControllerChest : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		animation["Close"].speed = -3;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Static.Variables.ContainsKey("finished_lily"))
		{
			//StartCoroutine(PlayAnim(2.0f));
			animation.Play("Close");
		}
	}

	IEnumerator PlayAnim(float delay)
	{
		yield return new WaitForSeconds(delay);
		animation.Play("Close");
	}
	

}
