using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
	private Animator anim;
	private bool areaPuerta;
	private bool door;
	public static bool llaveCogida;
	public float timeFinishPanel;
	public GameObject finishPanel;
	private bool exit;

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
			anim.SetBool("abierto", true);			

			timeFinishPanel = Time.time + 1f;
			exit = true;

			Destroy(GameObject.FindGameObjectWithTag("Llave"));
		}
		if (Time.time > timeFinishPanel && exit)
		{
			finishPanel.SetActive(true);
		}
	}
}
