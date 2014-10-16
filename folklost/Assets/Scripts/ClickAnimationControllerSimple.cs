using UnityEngine;
using System.Collections;

public class ClickAnimationControllerSimple : MonoBehaviour 
{
	public GameObject item;
	public string animationName;
	private bool playing;

	// Use this for initialization
	void Start () {
		playing = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver() 
	{
			if (Input.GetMouseButtonDown(0))
			{
				if(!playing)
				{
					item.animation.Play(animationName);
					playing = true;
				}
				else if(playing)
				{
					item.animation.Stop();
					playing = false;
				}

			}
	}

}
