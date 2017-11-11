﻿using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    public float movementSpeed = 10f;
    public float jumpHeight = 100f;
    float rotY = 0;

    const float eps = 0.0001f;
    const float minR = 10f;

    public Transform nose;
    public Transform tail;
    public Transform groundCheck;

    public LayerMask whatIsGround;

    bool isGrounded;

    public float rotationSpeed;
    public float rotationFriction;
    public float rotationSmoothness;

    private Quaternion rotateTo;

    Rigidbody rb;
    Animator anim;
    public GameManager gm;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        isGrounded = Physics.Linecast(transform.position, groundCheck.position, whatIsGround);
        rotY = Input.GetAxis("Horizontal");
        if (isGrounded)
        {
            rb.constraints = RigidbodyConstraints.None;
            anim.SetFloat("Direction", rotY);
            if (Mathf.Abs(rotY) > eps)
            {
                Turn();
            }

            float accelarate = Input.GetAxis("Fire1");
            if (accelarate > 0)
            {
                rb.AddForce((nose.position - tail.position) * movementSpeed * accelarate);
            }

            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
        }
        else // Will handle everthing while in the air
        {
            float flipDir = Input.GetAxis("Flipping"); //* rotationSpeed * rotationFriction;
            rotateTo = Quaternion.Euler(0, 0, flipDir);
            rb.constraints = RigidbodyConstraints.None;
            if(rotY != 0)
            {
                transform.Rotate(new Vector3(0, 10*rotY, 0));
            }
            if(flipDir != 0)
            {
                //transform.rotation = Quaternion.Lerp(transform.rotation, rotateTo, Time.deltaTime * rotationSmoothness);
                //transform.Rotate(new Vector3(0, 0, 10 * flipDir));
                transform.RotateAround(transform.position, Vector3.forward, flipDir*rotationSpeed);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gm.Respawn();
        }

    }
    void Turn()
    {
        anim.SetFloat("Direction", rotY);
        float speed = transform.InverseTransformDirection(rb.velocity).z;
        float radius = minR / rotY;
        Vector3 rotPoint = new Vector3(radius, 0, 0);
        float angle = speed * Time.deltaTime / radius * 180 / Mathf.PI;
        transform.RotateAround(transform.TransformPoint(rotPoint), Vector3.up, angle);
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }
}
