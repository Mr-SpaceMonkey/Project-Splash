using UnityEngine;

public class TrickManager : MonoBehaviour {

    PlayerMotor pm;
	public float manuscore;

	// Use this for initialization
	void Start ()
    {
        pm = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (pm.isManual) {
			manuscore = manuscore + Time.deltaTime;
		}
	}
}
