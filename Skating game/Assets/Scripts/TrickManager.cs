using UnityEngine;
using UnityEngine.UI;

public class TrickManager : MonoBehaviour {
<<<<<<< HEAD

    PlayerMotor pm;
=======
	PlayerMotor pm;
	ScoreManager sc;
>>>>>>> 562d997785a4044f18f66306e824cec08b92a67e
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
	public void LandManual()
	{
		sc.AddScore (100f);
	}
}
