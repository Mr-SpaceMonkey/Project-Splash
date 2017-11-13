using UnityEngine;

public class GrindCheck : MonoBehaviour
{

    PlayerMotor pm;

    private void Start()
    {
        pm = GetComponentInParent<PlayerMotor>();
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Inside function");
        if (collision.gameObject.tag == "Rail")
        {
            Debug.Log("Inside if");
            pm.isGrinding = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Rail")
        {
            pm.isGrinding = false;
        }
    }

}