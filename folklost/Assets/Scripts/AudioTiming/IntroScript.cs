using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Twine;

public class IntroScript : MonoBehaviour {

	public GameObject ship;
	public GameObject man;
	public GameObject player;
	public GameObject camera1;
	public GameObject camera2;
	private bool started;

	private GameObject hitObj;
	private RaycastHit hit;

	void Start(){
		started = false;
	}

	void Update(){
		Dialog dialog = man.GetComponent<Dialog>();


		/*Switch to a different camera if Dialog is running*/
		if (dialog.Open && started) {
			camera1.SetActive(false);
			camera2.SetActive(true);
				} 
		else {
			camera1.SetActive(true);
			camera2.SetActive(false);
				}

		if(!dialog.Open && started)
			audio.Stop();

	}


	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag.Equals("Player") && !started){

			started=true;
			man.SetActive(true);
			ship.SetActive(true);

			Dialog dialog = man.GetComponent<Dialog>();
			dialog.StartDialog();
			audio.Play();


			ship.SetActive(true);
		}
	}

//	IEnumerator BackUp(Vector3 pos, float time){
//		int x = 3;
//		for(int i = 0; i < x; i++){
//			yield return new WaitForSeconds(time);
//			player.transform.Translate(pos / x);
//		}
//	}
//
//	IEnumerator ShootRay(GameObject target){
//		while(!hitObj.Equals(man)){
//			Physics.Raycast (transform.position,transform.forward, out hit, 5);
//			hitObj = hit.collider.gameObject;
//		}
//		yield return 0;
//	}

}
