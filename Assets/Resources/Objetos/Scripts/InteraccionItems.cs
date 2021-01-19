using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionItems : MonoBehaviour
{
    #region Propiedades
    private GameObject llave;

	public ParticleSystem particulaCorazon;
	public bool inside;

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
				llave = obj.gameObject;
				break;
			case "Corazon":
				PlayerStats ps = this.GetComponent<PlayerStats>();
				if (!(ps.currentHP == ps.maxHP))
                {
					particulaCorazon.Play();
					ps.increaseHP(50);
					Destroy(obj.gameObject);
				}
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
    
	public void UsarLlave()
	{
		if (inside && Input.GetKeyDown(KeyCode.E))
		{
			Puerta.llaveCogida = true;
			llave.SetActive(false);
		}
	}
    #endregion
}
