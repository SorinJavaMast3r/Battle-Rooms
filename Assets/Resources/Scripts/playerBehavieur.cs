using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavieur : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 200.0f;
    public Animator anim;
    public float x, y;

    public Rigidbody rb;
    public float salto = 8f;
    public bool puedoSaltar;

    // Start is called before the first frame update
    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", x);
        anim.SetFloat("Vertical", y);

        if (puedoSaltar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("saltar", true);
                rb.AddForce(new Vector3(0, salto, 0), ForceMode.Impulse);
            }
            anim.SetBool("suelo", true);
        }
        else
        {
            EstoyCayendo();
        }

        
    }

    public void EstoyCayendo()
    {
        anim.SetBool("suelo", false);
        anim.SetBool("saltar", false);
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * movementSpeed);
    }
}
