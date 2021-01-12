﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorSkills : MonoBehaviour
{
    private bool animationExit = false;
    private float animationTime = 0.84f;
    private float animationStartTime;
    private Animator animator;
    private string keyPressed;
    private float abilityTime;

    public Skill energyBall;
    public Skill rayo;
    public Skill earthSmash;
    public GameObject player;

    private bool ataque;
    private float velocidad, velocidadGiro;
    private AnimationStateControllerMago controladorMago;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = player.GetComponent<Animator>();
        controladorMago = player.GetComponent<AnimationStateControllerMago>();
        velocidad = controladorMago.speed;
        velocidadGiro = controladorMago.rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && this.animator.GetFloat("cooldown") <= 0 && !ataque)
        {
            ataque = true;

            this.animator.SetBool("firstMagicAttack", ataque);
            
            animationStartTime = Time.time + animationTime;
            animationExit = true;
            keyPressed = "q";
            controladorMago.speed = 0;
            controladorMago.rotationSpeed = 0;
            abilityTime = Time.time + 2.0f;

            this.animator.SetFloat("cooldown", 200f);
        }
        
        this.animator.SetFloat("cooldown", this.animator.GetFloat("cooldown") - 1f);
        
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ataque = false;
            this.animator.SetBool("firstMagicAttack", ataque);
        }

        if (Input.GetKeyDown(KeyCode.E) && this.animator.GetFloat("cooldown") <= 0 && !ataque)
        {
            this.animator.SetBool("secondMagicAttack", true);
            animationStartTime = Time.time + animationTime;
            animationExit = true;
            keyPressed = "e";
            controladorMago.speed = 0;
            controladorMago.rotationSpeed = 0;
            abilityTime = Time.time + 3.5f;

            this.animator.SetFloat("cooldown", 200f);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            this.animator.SetBool("secondMagicAttack", false);
        }

        if (Input.GetKeyDown(KeyCode.R) && this.animator.GetFloat("cooldown") <= 0 && !ataque)
        {
            this.animator.SetBool("thirdMagicAttack", true);
            animationStartTime = Time.time + animationTime;
            animationExit = true;
            keyPressed = "r";
            controladorMago.speed = 0;
            controladorMago.rotationSpeed = 0;
            abilityTime = Time.time + 2.0f;

            this.animator.SetFloat("cooldown", 200f);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            this.animator.SetBool("thirdMagicAttack", false);
        }

    }

    void FixedUpdate()
    {
        if(Time.time > abilityTime)
        {
            controladorMago.speed = velocidad;
            controladorMago.rotationSpeed = velocidadGiro;
        }

        if (animationExit && Time.time > animationStartTime)
        {
            switch (keyPressed)
            {
                case "q":
                    this.energyBall.lanzarHabilidad(this.gameObject);
                    animationExit = false;
                    break;
                case "e":
                    this.rayo.lanzarHabilidad(this.gameObject);
                    animationExit = false;
                    break;
                case "r":
                    this.earthSmash.lanzarHabilidad(this.gameObject);
                    animationExit = false;
                    break;
            }
        }


    }

}
