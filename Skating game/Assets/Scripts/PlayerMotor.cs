﻿using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    [Header("Initialising Wheel Colliders")]
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider backLeft;
    public WheelCollider backRight;

    [Header("Movement Feel")]
    public float motorForce;
    public float steerForce;
    public float jumpHeight;
    public float shuvitSpeed;
    public float flipSpeed;
    public float impossibleSpeed;

    [Header("")]
    public Transform groundCheck;
    public LayerMask whatIsGround;

    Animator anim;
    Rigidbody rb;
    [HideInInspector]
    public bool isGrounded;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update ()
    {
        isGrounded = Physics.Linecast(transform.position, groundCheck.position, whatIsGround);
        float flip = Input.GetAxis("Flipping");
        float motor = Input.GetAxis("Fire1") * motorForce;
        float turn = Input.GetAxis("Horizontal") * steerForce;
        float impossible = Input.GetAxis("Vertical");
        anim.SetFloat("Direction", turn);

        if (isGrounded)
        {
            backRight.motorTorque = motor;
            frontLeft.motorTorque = motor;
            frontRight.motorTorque = motor;
            backLeft.motorTorque = motor;
            frontLeft.steerAngle = turn;
            frontRight.steerAngle = turn;
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
        }
        else
        {
            if(turn != 0)
            {
                transform.Rotate(0,shuvitSpeed*turn,0);
            }
            if(flip != 0)
            {
                transform.Rotate(0, 0, flipSpeed * flip);
            }
            if (impossible != 0)
            {
                transform.Rotate(impossible * impossibleSpeed, 0, 0);
            }
        }
	}
}
