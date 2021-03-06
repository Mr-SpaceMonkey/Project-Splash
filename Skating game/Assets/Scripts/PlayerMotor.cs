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
    public Vector3 cOM;
    public Vector3 cOMManual;
    public Vector3 cOMNoseManual;


    [Header("")]
    public Transform groundCheck;
    public Transform nearGroundCheck;
    public LayerMask whatIsGround;
	Vector3 tempPos;
	Vector3 tempVelocity;

    Animator anim;
    Rigidbody rb;
	TrickManager tm;

    [HideInInspector]
    public bool isGrounded;
    [HideInInspector]
	public bool isManual;
    bool isNearGround;
    [HideInInspector]
    public bool isGrinding;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = cOM;
    }

    // Update is called once per frame
    void Update ()
    {
        isGrounded = Physics.Linecast(transform.position, groundCheck.position, whatIsGround);
        isNearGround = Physics.Linecast(transform.position,nearGroundCheck.position,whatIsGround);
        float impossible = Input.GetAxis("Vertical");
        float flip = Input.GetAxis("Flipping");
        float motor = Input.GetAxis("Fire1") * motorForce;
        float turn = Input.GetAxis("Horizontal") * steerForce;
        anim.SetFloat("Direction", turn);

        if (isGrounded || isGrinding)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
        }

		if (isGrounded || isManual)
        {
            backRight.motorTorque = motor;
            frontLeft.motorTorque = motor;
            frontRight.motorTorque = motor;
            backLeft.motorTorque = motor;
            frontLeft.steerAngle = turn;
            frontRight.steerAngle = turn;
            backLeft.steerAngle = -turn;
            backRight.steerAngle = -turn;
        }
        else
        {
            if (turn != 0)
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
        if (isGrinding)
        {
            Debug.Log("Grinding");
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
        if (isNearGround)
        {
            if (rb.transform.eulerAngles.x >= 5f && rb.transform.eulerAngles.x <= 25f && Input.GetButton("Manual"))
            {
                rb.constraints = RigidbodyConstraints.FreezeRotationX;
                rb.centerOfMass = cOMNoseManual;
                Physics.IgnoreLayerCollision(8, 9);
                Debug.Log("Nose Manual, angle is "+rb.transform.eulerAngles.x);
				Debug.Log("Velocity is "+rb.velocity);
				tempVelocity = rb.velocity;
				if (tempVelocity.x > 0f) {
					tempVelocity.x = tempVelocity.x - 0.05f * Time.deltaTime;
				}
				if (tempVelocity.y > 0f) {
					tempVelocity.y = tempVelocity.y - 0.05f * Time.deltaTime;
				}
				if (tempVelocity.z > 0f) {
					tempVelocity.z = tempVelocity.z - 0.05f * Time.deltaTime;
				}
				rb.velocity = tempVelocity;
                isManual = true;
            }
            else if (rb.transform.eulerAngles.x <= 355f && rb.transform.eulerAngles.x >= 345f && Input.GetButton("Manual"))
            {
                rb.constraints = RigidbodyConstraints.FreezeRotationX;
                rb.centerOfMass = cOMManual;
                Physics.IgnoreLayerCollision(8, 9);
                isManual = true;
                Debug.Log("Manual");
            }
            else
            {
                rb.centerOfMass = cOM;
                rb.constraints = RigidbodyConstraints.None;
                if (isManual && Input.GetButtonUp("Manual"))
                {
                    rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                }
                isManual = false;
            }
        }
	}

    /*private void FixedUpdate()
    {
        if (isGrinding)
        {
            Debug.Log("grinding");
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rail")
        {
            isGrinding = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Rail")
        {
            isGrinding = false;
        }
    }

}
