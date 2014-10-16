using UnityEngine;
using System.Collections;
using RenPy;

public class AnimationControllerFerrymanCabin: MonoBehaviour {
	
	public bool turn_to = false; //DONE
	//public bool turn_from = false; //DONE
	public bool shoo_initial = false; //DONE
	public bool shoo_not_really = false; //DONE
	public bool shoo_brun = false; //DONE
	public bool sigh_final = false; //DONE
	public bool sigh_lord = false; //DONE
	public bool sigh_initial = false; //DONE
	public bool sigh_alice = false; //DONE
	public bool yell_alice = false; //DONE

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{	
		//Ferryman first sigh
		if(Static.Variables.ContainsKey("sigh_initial") && !sigh_initial)
		{
			animation["Ferryman_groan"].speed = 1f;
			animation.CrossFade("Ferryman_groan", 0);
			StartCoroutine(AnimationWaitSighInitial());
		}
		//Ferryman shoo initial
		else if(Static.Variables.ContainsKey("shoo_initial") && !shoo_initial)
		{
			animation["Ferryman_shoo"].speed = 1.5f;
			animation.CrossFade("Ferryman_shoo", 0);
			StartCoroutine(AnimationWaitShooInitial());
		}
		//Ferryman "Oh lord" sigh
		else if(Static.Variables.ContainsKey("sigh_lord") && !sigh_lord)
		{
			animation["Ferryman_groan"].speed = 1f;
			animation.CrossFade("Ferryman_groan", 0);
			StartCoroutine(AnimationWaitSighLord());
		}
		//Ferryman Not really label shoo
		else if(Static.Variables.ContainsKey("shoo_not_really") && !shoo_not_really)
		{
			animation["Ferryman_shoo"].speed = 1.5f;
			animation.CrossFade("Ferryman_shoo", 0);
			StartCoroutine(AnimationWaitShooNotReally());
		}
		//Ferryman Brunhilda shoo
		else if(Static.Variables.ContainsKey("shoo_brun") && !shoo_brun)
		{
			animation["Ferryman_shoo"].speed = 1.5f;
			animation.CrossFade("Ferryman_shoo", 0);
			StartCoroutine(AnimationWaitShooBrun());
		}
		//Ferryman turns to player
		else if(Static.Variables.ContainsKey("yell_alice") && !yell_alice)
		{
			animation["Ferryman_yell"].speed = .5f;
			animation.CrossFade("Ferryman_yell", 0);
			StartCoroutine(AnimationWaitYellAlice());
		}
//		//Ferryman turns to player
//		else if(Static.Variables.ContainsKey("turn_to") && !turn_to)
//		{
//			animation["Ferryman_pivotturn"].speed = 1f;
//			animation.CrossFade("Ferryman_pivotturn", 0);
//			StartCoroutine(AnimationWaitTurnTo());
//		}
//		else if(Static.Variables.ContainsKey("turn_from") && !turn_from)
//		{
//			animation["Ferryman_turnback"].speed = 1.5f;
//			animation.CrossFade("Ferryman_turnback", 0);
//			StartCoroutine(AnimationWaitTurnFrom());
//		}
		else if(Static.Variables.ContainsKey("sigh_final") && !sigh_final)
		{
			animation["Ferryman_groan"].speed = 1f;
			animation.CrossFade("Ferryman_groan", 0);
			StartCoroutine(AnimationWaitSighFinal());
		}
		else if(Static.Variables.ContainsKey("sigh_alice") && !sigh_alice)
		{
			animation["Ferryman_groan"].speed = 1f;
			animation.CrossFade("Ferryman_groan", 0);
			StartCoroutine(AnimationWaitSighAlice());
		}
		//default to idle
		else
		{
			animation.CrossFade("Ferryman_idle", .5f);
		}
	}

	private IEnumerator AnimationWaitSighInitial() 
	{
		yield return new WaitForSeconds(animation["Ferryman_groan"].length * .667f);
		sigh_initial = true;
	}
	private IEnumerator AnimationWaitSighLord() 
	{
		yield return new WaitForSeconds(animation["Ferryman_groan"].length * .667f);
		sigh_lord = true;
	}	

	private IEnumerator AnimationWaitShooInitial() 
	{
		yield return new WaitForSeconds(animation["Ferryman_shoo"].length* .5f);
		shoo_initial = true;
	}
	
	private IEnumerator AnimationWaitShooNotReally() 
	{
		yield return new WaitForSeconds(animation["Ferryman_shoo"].length* .5f);
		shoo_not_really = true;
	}
	
	private IEnumerator AnimationWaitShooBrun() 
	{
		yield return new WaitForSeconds(animation["Ferryman_shoo"].length* .5f);
		shoo_brun = true;
	}
	
	private IEnumerator AnimationWaitYellAlice() 
	{
		yield return new WaitForSeconds(animation["Ferryman_yell"].length * .5f);
		yell_alice = true;
	}
	
	private IEnumerator AnimationWaitTurnTo() 
	{
		yield return new WaitForSeconds(animation["Ferryman_pivotturn"].length * .667f);
		turn_to = true;
	}
	
//	private IEnumerator AnimationWaitTurnFrom() 
//	{
//		yield return new WaitForSeconds(animation["Ferryman_turnback"].length * .667f);
//		turn_from = true;
//	}
	
	private IEnumerator AnimationWaitSighFinal() 
	{
		yield return new WaitForSeconds(animation["Ferryman_groan"].length * .667f);
		sigh_final = true;
	}
	
	private IEnumerator AnimationWaitSighAlice() 
	{
		yield return new WaitForSeconds(animation["Ferryman_groan"].length * .667f);
		sigh_alice = true;
	}
}
