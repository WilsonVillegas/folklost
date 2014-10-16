using UnityEngine;
using System.Collections;

namespace Util {
	
	public class Interact {
		
		public static bool Raycast(out RaycastHit hitInfo, float raycastDistance) {
			if(Camera.main == null) {
				hitInfo = new RaycastHit();
				return false;
			}
			Vector3 direction = Camera.main.transform.forward;
			return Physics.Raycast(Camera.main.transform.position, direction, out hitInfo, raycastDistance);
		}
	}
}
