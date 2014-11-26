using UnityEngine;
using System.Collections;

public class ClickModelChange : MonoBehaviour {

	public Vector3[] positions;
	public Vector3[] rotations;
	public GameObject model;
	private int index = 0;

	// Use this for initialization
	void Start () 
	{
		//model.transform.position = positions[0];
		//model.transform.rotation.Set ( rotations[index].x, rotations[index].y, rotations[index].z, model.transform.rotation.w);
	}
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0))
		{
			animation.Play();
			//model.transform.position = positions[index];
		}
	}
}
