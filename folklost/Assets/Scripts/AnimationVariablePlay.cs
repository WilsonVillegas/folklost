using UnityEngine;
using System.Collections;
using RenPy;

public class AnimationVariablePlay : MonoBehaviour {


	// Update is called once per frame
	void Update () 
	{
		if(Static.Variables.ContainsKey("BirdAgain"))
		{
			animation.Play();
			StartCoroutine(FlyTime());
		}

	}

	public IEnumerator FlyTime()
	{
		yield return new WaitForSeconds(.4f);
		gameObject.SetActive(false);
	}
}
