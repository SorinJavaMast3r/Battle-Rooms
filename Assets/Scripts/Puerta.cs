using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
	private Animator anim;
	private bool areaPuerta;
	private bool door;
	public static bool llaveCogida;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider obj)
	{
		if (obj.tag == "Player")
		{
			areaPuerta = true;
		}
	}

	void OnTriggerExit(Collider obj)
	{
		if (obj.tag == "Player")
		{
			areaPuerta = false;
		}
	}

	void Update()
	{
		if (areaPuerta && Input.GetKeyDown(KeyCode.E) && llaveCogida)
		{
			door = !door;
		}
		if (door)
		{
			anim.SetBool("abierto", true);
		}
	}
}
