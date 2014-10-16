using UnityEngine;
using System.Collections;
using RenPy;

public class AnimationControllerDrunk : MonoBehaviour {

	public bool drunkard_drunk = false;
	public bool drunkard_hammered = false;
	public bool drunkard_napping = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Static.Variables.ContainsKey("drunkard_hammered"))
			drunkard_drunk = true;
		if(Static.Variables.ContainsKey("drunkard_drunk") && !drunkard_drunk)
		{
			animation["Drunk_happy"].speed = 1f;
			animation.CrossFade("Drunk_happy", .5f);
		}
		else if(Static.Variables.ContainsKey("drunkard_hammered") && !drunkard_napping)
		{
			animation["Drunk_sleeping"].speed = 1f;
			animation.CrossFade("Drunk_sleeping", .5f);
			StartCoroutine(AnimationWaitDrunkNap());
		}
		else
		{
			if(drunkard_napping)
				animation.Stop();
			else
				animation.CrossFade("Drunk_waiting", .5f);
		}
	}	

	private IEnumerator AnimationWaitDrunkNap() 
	{
		yield return new WaitForSeconds(animation["Drunk_sleeping"].length * .75F);
		drunkard_napping = true;
	}

}
