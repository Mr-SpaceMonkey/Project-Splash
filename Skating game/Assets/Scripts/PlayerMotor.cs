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
    public float grindSnapRange;

    [Header("")]
    public Transform groundCheck;
    public Transform nearGroundCheck;
    public LayerMask whatIsGround;
	public Vector3 tempPos;

    Animator anim;
    Rigidbody rb;
	TrickManager tm;

    [HideInInspector]
    public bool isGrounded;
    bool isNearGround;
    [HideInInspector]
	public bool isManual;

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
				transform.Rotate(0, 0, flipSpeed * flip);
            }
			if (impossible != 0)
            {
                transform.Rotate(impossible * impossibleSpeed, 0, 0);
            }
        }
		if (rb.transform.rotation.eulerAngles.x >= 5f && rb.transform.rotation.eulerAngles.x <= 30f && Input.GetButton("Manual") && isNearGround) {
			rb.constraints = RigidbodyConstraints.FreezeRotation;
			rb.constraints = RigidbodyConstraints.FreezePositionY;
			isManual= true;
			Physics.IgnoreLayerCollision (8, 9);
		}
        else if (rb.transform.rotation.eulerAngles.x <= 348f && rb.transform.rotation.eulerAngles.x >= 337f && Input.GetButton("Manual"))
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            isManual = true;
            Physics.IgnoreLayerCollision(8, 9);
        }
        else {
			rb.constraints = RigidbodyConstraints.None;
			if (isManual && Input.GetButtonUp ("Manual")) { 
				rb.AddForce (Vector3.up * jumpHeight * 1.5f, ForceMode.Impulse);
			}
			isManual = false;
		}
		if (rb.transform.rotation.eulerAngles.x >= 5f && rb.transform.rotation.eulerAngles.x <= 30f && Input.GetButtonDown("Manual") && isNearGround)
		{
			tm.LandManual ();
			tempPos = rb.transform.position;
			tempPos.y = tempPos.y - 0.6f;
			rb.transform.position = tempPos;
		}
		else if (rb.transform.rotation.eulerAngles.x <= 350f && rb.transform.rotation.eulerAngles.x >= 335f && Input.GetButtonDown("Manual") && isNearGround == true)
		{
			if (isNearGround == true) {
				rb.constraints = RigidbodyConstraints.FreezePositionY;
				tempPos = rb.transform.position;
				tempPos.y = tempPos.y - 0.6f;
				rb.transform.position = tempPos;
			}
		}
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, grindSnapRange);
    }
}
