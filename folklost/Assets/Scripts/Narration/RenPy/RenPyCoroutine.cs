using UnityEngine;
using System.Collections;

namespace RenPy
{
	public class RenPyCoroutine : MonoBehaviour
	{
		public Coroutine BeginCoroutine(IEnumerator routine) {
			StopAllCoroutines();
			return StartCoroutine(routine);
		}
	}
}
