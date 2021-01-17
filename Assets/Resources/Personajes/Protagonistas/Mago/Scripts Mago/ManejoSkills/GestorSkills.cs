using JetBrains.Annotations;
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
    private PlayerStats playerStats;

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
        playerStats = player.GetComponent<PlayerStats>();
        velocidad = controladorMago.speed; 
        velocidadGiro = controladorMago.rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && this.animator.GetFloat("cooldown") <= 0 && !ataque && playerStats.currentMP >= 7)
        {
            ataque = true;

            this.animator.SetBool("firstMagicAttack", ataque);
            
            animationStartTime = Time.time + animationTime;
            animationExit = true;
            keyPressed = "q";
            controladorMago.velocidad = 0;
            controladorMago.velocidadGiro = 0;
            abilityTime = Time.time + 2.0f;

            this.animator.SetFloat("cooldown", 200f);

            playerStats.decreaseMP(7);
        }
        
        this.animator.SetFloat("cooldown", this.animator.GetFloat("cooldown") - 1f);
        
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            ataque = false;
            this.animator.SetBool("firstMagicAttack", ataque);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && this.animator.GetFloat("cooldown") <= 0 && !ataque && playerStats.currentMP >= 20)
        {
            this.animator.SetBool("secondMagicAttack", true);
            animationStartTime = Time.time + animationTime;
            animationExit = true;
            keyPressed = "e";
            controladorMago.velocidad = 0;
            controladorMago.velocidadGiro = 0;
            abilityTime = Time.time + 3.5f;

            this.animator.SetFloat("cooldown", 200f);

            playerStats.decreaseMP(20);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            this.animator.SetBool("secondMagicAttack", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && this.animator.GetFloat("cooldown") <= 0 && !ataque && playerStats.currentMP >= 15)
        {
            this.animator.SetBool("thirdMagicAttack", true);
            animationStartTime = Time.time + animationTime;
            animationExit = true;
            keyPressed = "r";
            controladorMago.velocidad = 0;
            controladorMago.velocidadGiro = 0;
            abilityTime = Time.time + 2.0f;

            this.animator.SetFloat("cooldown", 200f);

            playerStats.decreaseMP(15);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            this.animator.SetBool("thirdMagicAttack", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !ataque)
        {
            velocidad = controladorMago.runSpeed;
            velocidadGiro = controladorMago.runningRotationSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !ataque)
        {
            velocidad = controladorMago.speed;
            velocidadGiro = controladorMago.rotationSpeed;
        }
    }

    void FixedUpdate()
    {
        if(Time.time > abilityTime)
        {
            controladorMago.velocidad = velocidad;
            controladorMago.velocidadGiro = velocidadGiro;
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
