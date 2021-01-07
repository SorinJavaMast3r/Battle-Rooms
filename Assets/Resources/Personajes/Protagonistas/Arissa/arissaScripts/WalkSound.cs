using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    public AudioSource foot;

	[Range(0.00f, 1.00f)]
	public float vol;

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Terrain")
		{
			foot.Play();
			foot.volume = vol;
		}
	}
}
