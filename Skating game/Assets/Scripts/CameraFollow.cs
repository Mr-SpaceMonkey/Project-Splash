using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float smoothSpeed;
    public float distanceAway;
    public float distanceUp;
    Vector3 targetPosition;

    void FixedUpdate()
    {
        targetPosition = target.position + target.up * distanceUp - target.forward * distanceAway;
        Debug.DrawRay(target.position, Vector3.up * distanceUp, Color.red);
        Debug.DrawRay(target.position, -1f * target.forward * distanceAway,Color.blue);
        Debug.DrawLine(target.position, targetPosition, Color.magenta);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        transform.LookAt(target);
    }

}
