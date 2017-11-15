using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public PlayerMotor pm;

    public Transform Nose;
    public Transform Tail;

    public float smoothSpeed;
    public float distanceAway;
    public float distanceUp;
    Vector3 targetPosition;

    void FixedUpdate()
    {
        if (pm.isGrounded)
        {
            targetPosition = target.position + target.up * distanceUp - target.forward * distanceAway;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
            transform.LookAt(target);
        }
        else
        {
            //targetPosition = target.position + Vector3.up * distanceUp / 1.5f - (Nose.position.normalized - Tail.position.normalized) * distanceAway * 2f;
            targetPosition = target.position + Vector3.up * distanceUp - (Nose.position-Tail.position).normalized * distanceAway;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
            transform.LookAt(target);
        }
    }

}
