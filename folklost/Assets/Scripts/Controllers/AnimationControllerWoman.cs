using UnityEngine;
using System.Collections;
using RenPy;

public class AnimationControllerWoman : MonoBehaviour {

	public bool Wwoman_fall = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Static.Variables.ContainsKey("alice_fade"))
		{
			animation["Wwoman_fall"].speed = .1f;
			animation.CrossFade("Wwoman_fall", 0f);
			StartCoroutine(AnimationWaitWomanFall());
		}
		else if(Static.Variables.ContainsKey("talked_alice"))
		{
			animation.CrossFade("Wwoman_talking", 0f);
		}

	}

	private IEnumerator AnimationWaitWomanFall()
	{
		yield return new WaitForSeconds(animation["Wwoman_fall"].length * 10f);
		Wwoman_fall = true;
	}
}
