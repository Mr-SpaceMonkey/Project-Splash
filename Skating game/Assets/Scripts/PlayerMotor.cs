using UnityEngine;

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
    public Transform nearGroundCheck;
    public LayerMask whatIsGround;

    Animator anim;
    Rigidbody rb;

    [HideInInspector]
    public bool isGrounded;
	bool isManual;
    bool isNearGround;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update ()
    {
        isGrounded = Physics.Linecast(transform.position, groundCheck.position, whatIsGround);
        isNearGround = Physics.Linecast(transform.position,nearGroundCheck.position,whatIsGround);
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
                rb.AddForce(Vector3.up * jumpHeight * 1.25f, ForceMode.Impulse);
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
				transform.Rotate(0, 0, flipSpeed *2f * flip);
            }
			if (impossible != 0)
            {
                transform.Rotate(impossible * impossibleSpeed *2f, 0, 0);
            }
        }
		if (rb.transform.rotation.eulerAngles.x >= 10f && rb.transform.rotation.eulerAngles.x <= 25f && Input.GetButton ("Manual")) {
			rb.constraints = RigidbodyConstraints.FreezeRotationX;
			isManual= true;
			Physics.IgnoreLayerCollision (8, 9);
		}
        else if (rb.transform.rotation.eulerAngles.x <= 10f && rb.transform.rotation.eulerAngles.x >= -25f && Input.GetButton("Manual"))
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
            isManual = true;
            Physics.IgnoreLayerCollision(8, 9);
        }
        else {
			rb.constraints = RigidbodyConstraints.None;
			if (isManual && Input.GetButtonUp ("Manual")) { 
				rb.AddForce (Vector3.up * jumpHeight, ForceMode.Impulse);
			}
			isManual = false;
		}
	}
}
