using UnityEngine;
using System.Collections;

public class ClickModelChange : MonoBehaviour {

	public Vector3[] positions;
	public Vector3[] rotations;
	public GameObject model;
	public GameObject[] wood;
	public GameObject axe;
	public GameObject blocker;
	public GameObject player;

	private int index = 0;

	void OnMouseOver()
	{

		if(Input.GetMouseButtonDown(0) && axe.activeSelf)
		{
			//if(index > 1)
			StartCoroutine(Fell ());
			index++;
		}
	}

	private IEnumerator Fell()
	{
		yield return new WaitForSeconds(1f);
		animation.Play ();
		wood[0].SetActive (true);
		wood[1].SetActive (true);
		wood[2].SetActive (true);
		yield return new WaitForSeconds(1f);
		ToastMachine.Instance.Toast("Picked up Silver Wood.");
		axe.SetActive (false);
		blocker.SetActive (false);
		player.audio.Stop ();
		audio.Play ();

	}
}
