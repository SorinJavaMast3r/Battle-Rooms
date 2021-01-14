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

    // Start is called before the first frame update
    void Start()
    {
        audioRun.volume = runVolume;
        audioWalk.volume = walkVolume;
        animator = GetComponent<Animator>();
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

            if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)) && !attack && !run)
            {
                attack = true;
            }

            if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Alpha3))
            {
                attack = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !attack && y > 0)
            {
                audioWalk.Stop();
                audioRun.Play();
                velocidad = runSpeed;
                velocidadGiro = runningRotationSpeed;
                run = true;
                animator.SetBool(anim_run, run);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                audioRun.Stop();
                if (y != 0)
                {
                    audioWalk.Play();
                }
                velocidad = speed;
                velocidad = rotationSpeed;
                run = false;
                animator.SetBool(anim_run, run);
            } // Correr
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
