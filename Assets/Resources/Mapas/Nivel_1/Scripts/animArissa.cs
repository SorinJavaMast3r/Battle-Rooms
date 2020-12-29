using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animArissa : MonoBehaviour
{
    [Header("Pameters")]
    public float movementSpeed = 2.0f;
    public float rotationSpeed = 200.0f;
    public int speedRun;
    public float initialSpeed;
    private Animator anim;
    public float x, y;
    void Start()
    {
        //Call arissa Animator
        anim = GetComponent<Animator>();
        initialSpeed = movementSpeed;
    }

    void Update()
    {
		if (Input.GetKey(KeyCode.LeftShift))
		{
            movementSpeed = speedRun;
            if (y > 0)
			{
                anim.SetBool("running", true);
			}
            else
			{
                anim.SetBool("running", false);
			}   
		}
        else
        {
            anim.SetBool("running", false);
            movementSpeed = initialSpeed;
        }
        //Player movement
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * movementSpeed);
        
        anim.SetFloat("speedX", x);
        anim.SetFloat("speedY", y);
    }
}
