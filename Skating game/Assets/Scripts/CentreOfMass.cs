using UnityEngine;

public class CentreOfMass : MonoBehaviour {

    public Vector3 cOM;
    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = cOM;
	}
	
}
