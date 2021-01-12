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

    public Skill energyBall;
    public Skill rayo;
    public Skill earthSmash;
    // Start is called before the first frame update
    void Start()
    {
        this.animator = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.animator.Play("First Magic Attack");
            animationStartTime = Time.time + animationTime;
            animationExit = true;
            keyPressed = "q";
            transform.parent.GetComponent<AnimationStateControllerMago>().setInterpolacion(0);
            transform.parent.GetComponent<AnimationStateControllerMago>().velocidadMovimiento = 0;
            abilityTime = Time.time + 2.0f;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            this.animator.Play("Second Magic Attack");
            animationStartTime = Time.time + animationTime;
            animationExit = true;
            keyPressed = "e";
            transform.parent.GetComponent<AnimationStateControllerMago>().setInterpolacion(0);
            transform.parent.GetComponent<AnimationStateControllerMago>().velocidadMovimiento = 0;
            abilityTime = Time.time + 3.5f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            this.animator.Play("Third Magic Attack");
            animationStartTime = Time.time + animationTime;
            animationExit = true;
            keyPressed = "r";
            transform.parent.GetComponent<AnimationStateControllerMago>().setInterpolacion(0);
            transform.parent.GetComponent<AnimationStateControllerMago>().velocidadMovimiento = 0;
            abilityTime = Time.time + 2.0f;
        }
    }

    void FixedUpdate()
    {
        if(Time.time > abilityTime)
        {
            transform.parent.GetComponent<AnimationStateControllerMago>().setInterpolacion(10);
            transform.parent.GetComponent<AnimationStateControllerMago>().velocidadMovimiento = 5.0f;
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
