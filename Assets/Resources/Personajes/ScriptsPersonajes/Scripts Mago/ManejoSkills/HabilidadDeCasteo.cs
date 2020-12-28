using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadDeCasteo : MonoBehaviour
{

    private Rigidbody efectoDentroDelJuego;

    public void Cast (Ray ray, float range, Rigidbody efecto)
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;
        Physics.Raycast(ray, range);

        efectoDentroDelJuego = Instantiate(efecto, transform.position, transform.rotation) as Rigidbody;

    }


}
