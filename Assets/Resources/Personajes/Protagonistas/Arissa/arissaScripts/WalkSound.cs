using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
	private void OnTriggerEnter(Collider obj)
	{
		Debug.Log("ESTÁ COLISIONANDO CON: " + obj.gameObject.tag);
	}
}
