using UnityEngine;
using System.Collections;

public class AnimationSpeedController : MonoBehaviour {

	public string animationName;
	public float speed;
	void Start() 
	{
		animation[animationName].speed = speed;
		animation.Play(animationName);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
