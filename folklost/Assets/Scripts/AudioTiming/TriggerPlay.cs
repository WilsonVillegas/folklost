using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Twine;

public class TriggerPlay : MonoBehaviour {

	private bool started;

	void Start(){
		started = false;
	}

	void Update(){

	}


	IEnumerator OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals("Player") && !started){
			started=true;
			yield return new WaitForSeconds(3);
			audio.Play();



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
