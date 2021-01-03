using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{

    [Header("Ajustes")]
    public int tiempo;
    public float velocidad;
    public Transform puntoGuarida;
    public NavMeshAgent agent;
    public Animator anim;

    [Header("Estados")]
    public bool idle;
    public bool atacar;
    public bool asustado;
    public int estado = 1;

    bool cambio = true;
    bool tiempodegiro;
    float y;
    Vector3 player;

    private void FixedUpdate()
    {
        tiempo += 1;

        if(estado == 1)
        {
            idle = true;
            atacar = false;
            asustado = false;
        } else if (estado == 2)
        {
            idle = false;
            atacar = true;
            asustado = false;
        } else if (estado == 3)
        {
            idle = false;
            atacar = false;
            asustado = true;
        }

        if (idle)
        {
            transform.Translate(Vector3.forward * velocidad * Time.fixedDeltaTime);
            transform.Rotate(new Vector3(0, y, 0));
            anim.SetBool("Caminar", true);
            anim.SetBool("Correr", false);
            if (tiempo >= Random.Range(100, 2500))
            {
                Girar();
                tiempo = 0;
                tiempodegiro = true;
            }

            if (tiempodegiro)
            {
                if (tiempo > Random.Range(10, 30))
                {
                    y = 0;
                    tiempodegiro = false;
                }
            }
        } else if (atacar)
        {
            anim.SetBool("Caminar", false);
            anim.SetBool("Correr", true);
            player = GameObject.FindGameObjectWithTag("Player").transform.position;
            agent.SetDestination(player);
            if(Vector3.Distance(player, transform.position) > 30)
            {
                estado = 1;
                cambio = true;
            }
        } else if (asustado)
        {
            anim.SetBool("Caminar", false);
            anim.SetBool("Correr", true);
            agent.SetDestination(puntoGuarida.transform.position);
            if (Vector3.Distance(puntoGuarida.position, transform.position) < 3)
            {
                anim.SetBool("Caminar", false);
                anim.SetBool("Correr", false);
                if (tiempo > Random.Range(500, 1000))
                {
                    estado = 1;
                }
            }
        }
    }

    public void Girar()
    {
        y = Random.Range(-3, 3);
    }

    public void CambiarEstado()
    {
        estado = Random.Range(1, 3);
        if(estado == 2)
        {
            cambio = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && cambio)
        {
            CambiarEstado();
        }
    }
}
