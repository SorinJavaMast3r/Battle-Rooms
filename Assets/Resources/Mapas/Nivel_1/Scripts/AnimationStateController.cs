using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Rigidbody _rb;
    //[Header ("Camera properties")]
    #region Camera
    private Camera _cam;
    private CameraMovement _camMov;
    private Vector3 camFwd;
    #endregion

    [Header ("Movement properties")]
    #region Movement
    [Range(1.0f, 10.0f)]
    public float walk_speed;
    [Range(1.0f, 10.0f)]
    public float backwards_walk_speed;
    [Range(1.0f, 10.0f)]
    public float strafe_speed;
    [Range(0.1f, 1.5f)]
    public float rotation_speed;
    //[Range(2.0f, 10.0f)]
    //public float jump_force;
    #endregion

    #region Animations
    //private MyTPCharacter tpc;
    private bool walking = false;
    private bool strateLeft = false;
    private bool strateRight = false;
    private bool backwards = false;
    private bool jump = false;
	#endregion

	private void Awake()
	{
		
	}
	private Animator animator;
    int estaMoviendoHash;
    int estaCorriendoHash;
    int saltarHash;
    public float velocidadMovimiento = 5.0f;
    private float current_x = 0, current_y = 0, interpolacion = 10;
    private Vector3 direccionActual = Vector3.zero;
    public GameObject posCam;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        estaMoviendoHash = Animator.StringToHash("isWalking");
        estaCorriendoHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool pulsarBoton = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        bool estaMoviendo = animator.GetBool(estaMoviendoHash);
        bool correrPulsado = Input.GetKey("left shift");
        bool estaCorriendo = animator.GetBool(estaCorriendoHash);

        if (!estaMoviendo && pulsarBoton)
        {
            animator.SetBool(estaMoviendoHash, true);
        }

        if (estaMoviendo && !pulsarBoton)
        {
            animator.SetBool(estaMoviendoHash, false);
        }

        if (!estaCorriendo && (estaMoviendo && correrPulsado))
        {
            animator.SetBool(estaCorriendoHash, true);
        }

        if(estaCorriendo && (!estaMoviendo || !correrPulsado))
        {
            animator.SetBool(estaCorriendoHash, false);
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Transform camera = Camera.main.transform;

        current_x = Mathf.Lerp(current_x, x, Time.deltaTime * interpolacion);
        current_y = Mathf.Lerp(current_y, y, Time.deltaTime * interpolacion);

        Vector3 direccion = camera.forward * current_y + camera.right * current_x;

        float longitudDireccion = direccion.magnitude;
        direccion.y = 0;
        direccion = direccion.normalized * longitudDireccion;

        if (direccion != Vector3.zero)
        {
            direccionActual = Vector3.Slerp(direccionActual, direccion, Time.deltaTime * interpolacion);

            posCam.transform.SetParent(null);

            transform.rotation = Quaternion.LookRotation(direccionActual);

            posCam.transform.SetParent(transform);

            transform.position += direccionActual * velocidadMovimiento * Time.deltaTime;
        }

        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam.transform.position, 1.0f);
    }
}
