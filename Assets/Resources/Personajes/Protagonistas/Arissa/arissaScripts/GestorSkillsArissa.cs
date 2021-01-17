using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorSkillsArissa : MonoBehaviour
{
    private bool animationExit = false;
    private float animationTime = 0.84f;
    private float animationStartTime;
    private Animator animator;
    private string keyPressed;
    private float abilityTime;
    private PlayerStats playerStats;

    public Skill flecha;
    public GameObject player;

    private arissaController controladorArrisa;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = player.GetComponent<Animator>();
        controladorArrisa = player.GetComponent<arissaController>();
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.dead)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && this.animator.GetFloat("cooldown") <= 0 && playerStats.currentMP >= 7)
        {
            animationStartTime = Time.time + animationTime;
            animationExit = true;
            keyPressed = "q";

            this.animator.SetFloat("cooldown", 200f);

            playerStats.decreaseMP(7);
        }

        this.animator.SetFloat("cooldown", this.animator.GetFloat("cooldown") - 1f);

        //if (Input.GetKeyDown(KeyCode.Alpha2) && this.animator.GetFloat("cooldown") <= 0 && playerStats.currentMP >= 20)
        //{
        //    this.animator.Play("Second Magic Attack");
        //    animationStartTime = Time.time + animationTime;
        //    animationExit = true;
        //    keyPressed = "e";

        //    this.animator.SetFloat("cooldown", 200f);

        //    playerStats.decreaseMP(20);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3) && this.animator.GetFloat("cooldown") <= 0 && playerStats.currentMP >= 15)
        //{
        //    this.animator.Play("Third Magic Attack");
        //    animationStartTime = Time.time + animationTime;
        //    animationExit = true;
        //    keyPressed = "r";

        //    this.animator.SetFloat("cooldown", 200f);

        //    playerStats.decreaseMP(15);
        //}

        if (animationExit && Time.time > animationStartTime)
        {
            switch (keyPressed)
            {
                case "q":
                    this.flecha.lanzarHabilidad(this.gameObject);
                    animationExit = false;
                    break;
                //case "e":
                //    this.rayo.lanzarHabilidad(this.gameObject);
                //    animationExit = false;
                //    break;
                //case "r":
                //    this.earthSmash.lanzarHabilidad(this.gameObject);
                //    animationExit = false;
                //    break;
            }
        }
    }

}
