using UnityEngine;
using System.Collections;

public class kILLER : MonoBehaviour
{

	public float timer = 5;


	void Start ()
	{
		Invoke ("kill", timer);
	}


	void kill ()
	{
		Destroy (gameObject);
	}
}
