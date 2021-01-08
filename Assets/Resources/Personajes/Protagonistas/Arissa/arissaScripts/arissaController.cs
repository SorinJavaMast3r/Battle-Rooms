﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arissaController : MonoBehaviour
{
    #region Prop menu pausa
    public bool MenuPausaActivo;
    public GameObject MenuPausa;
    public static arissaController instanciar;
    #endregion

    private const string anim_speed = "speed",
                         anim_horiz = "horizontal",
                         anim_vert = "vertical",
                         anim_attack = "arrow_shot",
                         anim_melee = "melee_kick",
                         anim_die = "die",
                         anim_run = "run";
    private Animator anim;
    public AudioSource audioRun, audioWalk;
    [Range(0.00f, 1.00f)]
    public float runVolume, walkVolume;
    public float 
        speed = 2.0f,
        rotationSpeed = 60.0f,
        x = 0.0f, 
        y = 0.0f;

    public bool
        attack = false,
        melee = false,
        jump = false,  //TODO: ?
        die = false,
        run = false,
        val = false,
        dead = false; 

    public Vector3 moveDirection = Vector3.zero;

    public void Awake()
    {
        instanciar = this;
    }

    void Start()
    {
        audioRun.volume = runVolume;
        audioWalk.volume = walkVolume;
        anim = GetComponent<Animator>();
        //Cuando animemos la muerte mirar linea 47
        Sonido.llamar.PlayAmbientSound();//Cambiar cuando esten los sonidos del juego
    }

    void Update()
    {
        
        AbrirMenuPausa();
        if (!MenuPausaActivo)
        {
            if (dead) // Si está muerto, hace animación de muerte y no hace nada más
            {
                if (die)
                {
                    anim.SetBool(anim_die, die);
                    die = !die;
                }
                return;
            }

            // Si pasa de aquí el personaje está vivo
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                audioWalk.Play();
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                audioWalk.Stop();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && !attack && !run)
            {
                attack = true;
                anim.SetBool(anim_attack, attack);
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                attack = false;
                anim.SetBool(anim_attack, attack);
            } // Disparo con arco

            if (Input.GetKeyDown(KeyCode.Mouse2) && !melee && !run)
            {
                melee = true;
                anim.SetBool(anim_melee, melee);
            }
            if (Input.GetKeyUp(KeyCode.Mouse2))
            {
                melee = false;
                anim.SetBool(anim_melee, melee);
            } // Patada

            if (Input.GetKeyDown(KeyCode.LeftShift) && !attack && !melee && y > 0)
            {
                audioWalk.Stop();
                audioRun.Play();           
                speed = 8.0f;
                rotationSpeed = 110.0f;
                run = true;
                anim.SetBool(anim_run, run);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                audioRun.Stop();
                if (y != 0)
				{
                    audioWalk.Play();
				}
                speed = 2.0f;
                rotationSpeed = 60.0f;
                run = false;
                anim.SetBool(anim_run, run);
            } // Correr
        }        
    }

	private void FixedUpdate()
	{
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        // Con Input.GetAxis("x"); obtenemos un movimiento suavizado, con GetAxisRaw son movimientos más agravantes. (l78 - l79)
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * speed);
        anim.SetFloat(anim_horiz, x);
        anim.SetFloat(anim_vert, y);
    }

    public void AbrirMenuPausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuPausaActivo = !MenuPausaActivo;

            if (MenuPausaActivo)
            {
                Sonido.llamar.PlayMusic();
                MenuPausa.gameObject.SetActive(true);
            }
            else
            {
                MenuPausa.gameObject.SetActive(false);
                Sonido.llamar.StopMusic();
            }
        }
    }

	
}
