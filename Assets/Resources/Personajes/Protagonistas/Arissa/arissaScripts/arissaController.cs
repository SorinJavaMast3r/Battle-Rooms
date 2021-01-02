using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arissaController : MonoBehaviour
{
    private const string anim_speed = "speed",
                         anim_horiz = "horizontal",
                         anim_vert = "vertical",
                         anim_attack = "arrow_shot",
                         anim_melee = "melee_kick",
                         anim_die = "die",
                         anim_run = "run";
    private Animator anim;

    public float 
        speed = 5.0f, 
        horizontal = 0.0f, 
        vertical = 0.0f;

    public bool 
        attack = false,
        melee = false,
        jump = false,  //TODO: ?
        die = false, 
        run = false,
        dead = false; // TODO:

    public Vector3 moveDirection = Vector3.zero;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
		if (dead)
		{
			if (die)
			{
                anim.SetBool(anim_die, true);
                die = !die;
			}
            return;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            attack = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            attack = false;
        }
        anim.SetBool(anim_attack, attack);
        if (Input.GetKeyDown(KeyCode.K))
        {
            melee = true;
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            melee = false;
        }
        anim.SetBool(anim_melee, melee);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            run = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            run = false;
        }
        anim.SetBool(anim_run, run);
    }
}
