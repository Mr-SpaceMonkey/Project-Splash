using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    [Header("Initialising Wheel Colliders")]
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider backLeft;
    public WheelCollider backRight;

    public float motorForce;
    public float steerForce;

    // Update is called once per frame
    void Update ()
    {
        float motor = Input.GetAxis("Fire1") * motorForce;
        float turn = Input.GetAxis("Horizontal") * steerForce;

        backLeft.motorTorque = motor;
        backRight.motorTorque = motor;
        frontLeft.motorTorque = motor;
        frontRight.motorTorque = motor;

        frontLeft.steerAngle = turn;
        frontRight.steerAngle = turn;
	}
}
