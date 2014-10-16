using UnityEngine;
using System.Collections;

public class ClickAnimationController : MonoBehaviour 
{
	public GameObject item;
	public string animationName;
	public bool closed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver() 
	{

		if(closed)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log("Opening");
				item.animation[animationName].speed = 1;
				item.animation.Play(animationName);
				//StartCoroutine(Moment());
				closed = false;
			}
		}
		else if(!closed)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log("Closing");
				item.animation[animationName].speed = -.5f;
				item.animation.Play(animationName);
				//StartCoroutine(Moment());
				closed = true;
			}
		}
	}

	private IEnumerator Moment()
	{
		Debug.Log("Waiting 1");
		yield return new WaitForSeconds(1);
		Debug.Log("Waiting 2"); 
	}
}
