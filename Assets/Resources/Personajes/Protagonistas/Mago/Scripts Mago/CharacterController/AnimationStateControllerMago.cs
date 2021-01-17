using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateControllerMago : MonoBehaviour
{

    private Animator animator;

    public float
        speed = 2.0f,
        runSpeed = 8.0f,
        velocidad,
        rotationSpeed = 60.0f,
        runningRotationSpeed = 110.0f,
        velocidadGiro,
        x = 0.0f,
        y = 0.0f;

    private const string anim_speed = "speed",
                         anim_horiz = "horizontal",
                         anim_vert = "vertical",
                         anim_die = "die",
                         anim_run = "run";

    public Vector3 moveDirection = Vector3.zero;

    public AudioSource audioRun, audioWalk;
    [Range(0.00f, 1.00f)]
    public float runVolume, walkVolume;

    public bool
        attack = false,
        jump = false,  //TODO: ?
        die = false,
        run = false,
        val = false,
        dead = false;

    private float abilityTime;

    // Start is called before the first frame update
    void Start()
    {
        audioRun.volume = runVolume;
        audioWalk.volume = walkVolume;
        animator = GetComponent<Animator>();
        this.velocidad = this.speed;
        this.velocidadGiro = this.rotationSpeed;
    }

    void Update()
    {
        if (!AbrirMenu.instanciar.MenuPausaActivo)
        {
            if (dead) // Si está muerto, hace animación de muerte y no hace nada más
            {
                if (die)
                {
                    animator.SetBool(anim_die, die);
                    die = !die;
                }
                return;
            }

            //Si pasa de aquí el personaje está vivo
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                audioWalk.Play();
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                audioWalk.Stop();
            }

            if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)) && !attack)
            {
                attack = true;

                if (run)
                {
                    run = false;
                    animator.SetBool(anim_run, run);
                    audioRun.Stop();
                }
                
                this.velocidad = 0;
                this.velocidadGiro = 0;

                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha3))
                {
                    abilityTime = Time.time + 2.0f;
                } else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    abilityTime = Time.time + 3.5f;
                }
            }

            if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Alpha3))
            {
                attack = false;
            }

            if (Time.time > abilityTime)
            {
                if (run)
                {
                    this.velocidad = runSpeed;
                    this.velocidadGiro = runningRotationSpeed;
                } else
                {
                    this.velocidad = speed;
                    this.velocidadGiro = rotationSpeed;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !attack && y > 0 && Time.time > abilityTime)
            {
                audioWalk.Stop();
                audioRun.Play();
                velocidad = runSpeed;
                velocidadGiro = runningRotationSpeed;
                run = true;
                animator.SetBool(anim_run, run);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && !attack && Time.time > abilityTime)
            {
                audioRun.Stop();
                if (y != 0)
                {
                    audioWalk.Play();
                }
                velocidad = speed;
                velocidadGiro = rotationSpeed;
                run = false;
                animator.SetBool(anim_run, run);
            } // Correr

            if(y == 0)
            {
                audioRun.Stop();
                audioWalk.Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        // Con Input.GetAxis("x"); obtenemos un movimiento suavizado, con GetAxisRaw son movimientos más agravantes. (l78 - l79)
        transform.Rotate(0, x * Time.deltaTime * velocidadGiro, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidad);
        animator.SetFloat(anim_horiz, x);
        animator.SetFloat(anim_vert, y);
    }
}
