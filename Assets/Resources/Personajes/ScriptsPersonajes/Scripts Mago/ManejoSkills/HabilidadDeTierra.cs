using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadDeTierra : MonoBehaviour
{
    public void Cast(Rigidbody efecto)
    {
        Rigidbody efectoDentroDelJuego = Instantiate(efecto, transform.position - (new Vector3(0,1,0)) + transform.forward, transform.rotation) as Rigidbody;
    }
}
