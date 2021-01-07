using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    public AudioSource foot;

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Terrain")
		{
			foot.Play();
		}
	}
}
