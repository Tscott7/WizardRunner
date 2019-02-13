using UnityEngine;
using System.Collections;

public class EndSpace : MonoBehaviour
{

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Player") {
			StartCoroutine (End ());
		}
	}

	IEnumerator End ()
	{
		yield return new WaitForSeconds (1);
	}
}
