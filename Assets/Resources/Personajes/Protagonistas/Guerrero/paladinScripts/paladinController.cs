using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paladinController : MonoBehaviour
{
    private const string 
        anim_speed = "Speed",
        anim_horiz = "Horizontal",
        anim_vert = "Vertical",
        anim_attack2 = "slash2",
        anim_kick = "kick",
        anim_die = "die",
        anim_run = "run";
    private Animator anim;
    public AudioSource audioRun, audioWalk, audioAttack1, audioAttack2, audioDeath, audioKick;
    [Range(0.00f, 1.00f)]
    public float runVolume, walkVolume, attackVolume, kickVolume, deathVolume; // TODO: ?
    public float
        speed = 1.5f, // El paladin es más lento porque lleva una armadura de metal
        rotationSpeed = 60.0f,
        x = 0.0f,
        y = 0.0f;
    public bool 
        attack1 = false,
        attack2= false,
        kick = false,
        die = false,
        run = false,
        dead = false,
        changeAttack = false;

    void Start()
    {
        audioRun.volume = runVolume;
        audioWalk.volume = walkVolume;
        audioAttack1.volume = attackVolume;
        audioAttack2.volume = attackVolume;
        audioKick.volume = kickVolume;
        audioDeath.volume = deathVolume;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!AbrirMenu.instanciar.MenuPausaActivo)
        {
            if (dead)
            {
                if (die)
                {
                    anim.SetBool(anim_die, die);
                    die = !die;
                }
                return;
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                audioWalk.Play();
            } // Reproducir sonido de los pasos cuando camina.
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                audioWalk.Stop();
            } // Parar el sonido de los pasos cuando camina.
            
            if (Input.GetKeyDown(KeyCode.Mouse0) && !attack2 && !run)
            {
                attack2 = true;
                anim.SetBool(anim_attack2, attack2);
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                attack2 = false;
                anim.SetBool(anim_attack2, attack2);
            } // Animación del golpe de espada
            if (Input.GetKeyDown(KeyCode.Mouse2) && !kick && !run)
            {
                kick = true;
                anim.SetBool(anim_kick, kick);
            }
            if (Input.GetKeyUp(KeyCode.Mouse2))
            {
                kick = false;
                anim.SetBool(anim_kick, kick);
            } // Animación de la patada
            if (Input.GetKeyDown(KeyCode.LeftShift) && !attack1 && !attack2 && !kick && y > 0)
            {
                audioWalk.Stop();
                audioRun.Play();
                speed = 4.5f; // Más lento que los demás por la armadura.
                rotationSpeed = 110.0f;
                run = true;
                anim.SetBool(anim_run, run);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                audioRun.Stop();
                if (y != 0)
                {
                    audioWalk.Stop();
                    audioWalk.Play();
                }
                speed = 1.5f;
                rotationSpeed = 60.0f;
                run = false;
                anim.SetBool(anim_run, run);
            } // Animación de correr
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
}
