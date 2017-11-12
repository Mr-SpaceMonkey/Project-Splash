using UnityEngine;

public class TrickManager : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(transform.rotation.z > 10)
        {
            Debug.Log("We did a flip");
        }
	}
}
