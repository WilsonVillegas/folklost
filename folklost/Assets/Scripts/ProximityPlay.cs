using UnityEngine;
using System.Collections;

public class ProximityPlay : MonoBehaviour 
{
	
	// Song to be played
	public AudioClip Theme;
	
	//Only once though
	private bool played = false;
	
	
	
	
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(played == false)
		{
		audio.PlayOneShot(Theme);
		played = true;
		}
    }
	
	
}
