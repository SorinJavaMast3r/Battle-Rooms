using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionItems : MonoBehaviour
{
	public GameObject llave;

	public ParticleSystem particulaCorazon;
	public bool inside;
	private Renderer visible;

	public static InteraccionItems llamar;

	public void Awake()
	{
		llamar = this;
	}
	void Start()
	{
		particulaCorazon.Stop();
		visible = llave.gameObject.GetComponent<Renderer>();
	}

	void Update()
	{
		UsarLlave();
	}

	public void OnTriggerEnter(Collider obj)
	{
		switch (obj.tag)
		{
			/*case "Pocion_vida":
				Inventario.llamar.AddItem(1);
				Destroy(obj.gameObject);
				break;
			case "Pocion_quita_vida":
				Inventario.llamar.AddItem(2);
				Destroy(obj.gameObject);
				break;*/
			case "Llave":
				//Inventario.llamar.AddItem(3);
				inside = true;
				break;
			case "Corazon":
				Player.vida += 1.0f;
				particulaCorazon.Play();
				Destroy(obj.gameObject);
				break;
				/*case "Trampa":
					vida -= 2.0f;
					Destroy(obj.gameObject);
					break;*/
		}
	}

	private void OnTriggerExit(Collider obj)
	{
		if (obj.tag == "Llave")
		{
			inside = false;
		}
	}

	public void PocionVida()
	{
		Player.vida++;
	}
	public void PocionMana()
	{
		Player.mana++;
	}

	public void UsarLlave()
	{
		if (inside && Input.GetKeyDown(KeyCode.E))
		{
			Puerta.llaveCogida = true;
			visible.enabled = false;
		}
	}
}
