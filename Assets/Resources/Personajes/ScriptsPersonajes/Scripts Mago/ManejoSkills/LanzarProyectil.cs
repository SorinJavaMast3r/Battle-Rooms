using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzarProyectil : MonoBehaviour
{
    public void Lanzar(Rigidbody proyectil, float velocidad)
    {
        Rigidbody proyectilDentroDelJuego = Instantiate(proyectil, transform.position, transform.rotation) as Rigidbody;

        proyectilDentroDelJuego.AddForce(transform.forward * velocidad, ForceMode.Impulse);
    }
}
