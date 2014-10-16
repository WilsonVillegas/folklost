using UnityEngine;
using System.Collections;
using RenPy;

public class AnimationControllerFerrymanDock: MonoBehaviour {
	
	public bool beckon = false;
	public bool shoo = false;

	void Update () 
	{
		//Ferryman beckon
		if(Static.Variables.ContainsKey("beckon") && !beckon)
		{
//			animation["Ferryman_pointcome"].speed = 1.75f;
//			animation.CrossFade("Ferryman_pointcome", 0);
//			StartCoroutine(AnimationWait());

			beckon = true;
		}
		//Ferryman Shoo
		else if(Static.Variables.ContainsKey("shoo") && !shoo && beckon)
		{
			animation["Ferryman_shoo"].speed = 1.75f;
			animation.CrossFade("Ferryman_shoo", 0);
			//Static.Variables["Beckon"] = "False";
			StartCoroutine(AnimationWait());
		}
		//Idle
		else
		{
			animation.CrossFade("Ferryman_idle", .5f);
		}
	}

	private IEnumerator AnimationWait() 
	{
		if(!shoo && beckon)
		{
			yield return new WaitForSeconds(animation["Ferryman_shoo"].length* .5f);
			shoo = true;
		}
		if(!beckon)
		{
			//wait for length of adjusted animation
			yield return new WaitForSeconds(animation["Ferryman_pointcome"].length * .25f);
			beckon = true;
		}

	}

}
