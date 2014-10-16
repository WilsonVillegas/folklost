using UnityEngine;
using System.Collections;
using RenPy;

public class AnimationController : MonoBehaviour {
	
	public bool lily_cower = false;
	public bool lily_idle = false;
	public bool lily_happy = false;
	public bool lily_sad = false;
	
	// Update is called once per frame
	void Update () 
	{
		//Lily cowers
		if(Static.Variables.ContainsKey("lily_cower") && !lily_cower)
		{
			animation["Lily_Cowering"].speed = 1f;
			animation.CrossFade("Lily_Cowering", 0);
			StartCoroutine(AnimationWaitLilyCower());
		}
		else if(Static.Variables.ContainsKey("lily_sad") && !lily_sad)
		{
			animation["Lily_buryingheadTeds"].speed = .75f;
			animation.CrossFade("Lily_buryingheadTeds", .2f);
			StartCoroutine(AnimationWaitLilySad());
		}
		else
		{
			if(Static.Variables.ContainsKey("lily_happy"))
	   			animation.CrossFade("Lily_Happy", .5f);
	 		else if(lily_cower)
				animation.CrossFade("Lily_Idle", .5f);
		}
	}
	
	private IEnumerator AnimationWaitLilyCower() 
	{
		yield return new WaitForSeconds(animation["Lily_Cowering"].length * .5f);
		lily_cower = true;
	}
	
	private IEnumerator AnimationWaitLilySad() 
	{
		yield return new WaitForSeconds(animation["Lily_buryingheadTeds"].length * .75f);
		lily_sad = true;
	}
	
}
