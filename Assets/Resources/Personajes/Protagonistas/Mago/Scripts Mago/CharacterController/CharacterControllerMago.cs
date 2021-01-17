using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMago : MonoBehaviour
{

    private Animator animator;
    int estaMoviendoHash;
    int estaCorriendoHash;
    public float velocidadMovimiento = 5.0f;
    private float current_x = 0, current_y = 0, interpolacion = 10;
    private Vector3 direccionActual = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        estaMoviendoHash = Animator.StringToHash("isWalking");
        estaCorriendoHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool pulsarBoton = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        bool estaMoviendo = animator.GetBool(estaMoviendoHash);
        bool correrPulsado = Input.GetKey("left shift");
        bool estaCorriendo = animator.GetBool(estaCorriendoHash);

        if (!estaMoviendo && pulsarBoton)
        {
            animator.SetBool(estaMoviendoHash, true);
        }

        if (estaMoviendo && !pulsarBoton)
        {
            animator.SetBool(estaMoviendoHash, false);
        }

        if (!estaCorriendo && (estaMoviendo && correrPulsado))
        {
            animator.SetBool(estaCorriendoHash, true);
        }

        if (estaCorriendo && (!estaMoviendo || !correrPulsado))
        {
            animator.SetBool(estaCorriendoHash, false);
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Transform camera = Camera.main.transform;

        current_x = Mathf.Lerp(current_x, x, Time.deltaTime * interpolacion);
        current_y = Mathf.Lerp(current_y, y, Time.deltaTime * interpolacion);

        Vector3 direccion = camera.forward * current_y + camera.right * current_x;

        float longitudDireccion = direccion.magnitude;
        direccion.y = 0;
        direccion = direccion.normalized * longitudDireccion;

        if (direccion != Vector3.zero)
        {
            direccionActual = Vector3.Slerp(direccionActual, direccion, Time.deltaTime * interpolacion);

            transform.rotation = Quaternion.LookRotation(direccionActual);

            transform.position += direccionActual * velocidadMovimiento * Time.deltaTime;
        }
    }

    public void setInterpolacion(float valor)
    {
        this.interpolacion = valor;
    }
}