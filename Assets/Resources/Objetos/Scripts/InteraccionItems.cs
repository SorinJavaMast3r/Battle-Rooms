using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionItems : MonoBehaviour
{
    #region Propiedades
    public GameObject llave;

	public ParticleSystem particulaCorazon;
	public bool inside;
	private Renderer visible;

	public static InteraccionItems llamar;
    #endregion

    #region Inicio y Update
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
    #endregion

    #region Trigger
    public void OnTriggerEnter(Collider obj)
	{
		switch (obj.tag)
		{
			case "Llave":
				inside = true;
				break;
			case "Corazon":
				Player.vida += 1.0f;
				particulaCorazon.Play();
				Destroy(obj.gameObject);
				break;
		}
	}

	private void OnTriggerExit(Collider obj)
	{
		if (obj.tag == "Llave")
		{
			inside = false;
		}
	}
    #endregion

    #region Uso de objetos
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
    #endregion
}
