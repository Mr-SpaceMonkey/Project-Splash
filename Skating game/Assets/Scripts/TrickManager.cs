using UnityEngine;
using UnityEngine.UI;

public class TrickManager : MonoBehaviour {
	PlayerMotor pm;
	ScoreManager sc;
	public float manuscore;

	// Use this for initialization
	void Start ()
    {
		
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
