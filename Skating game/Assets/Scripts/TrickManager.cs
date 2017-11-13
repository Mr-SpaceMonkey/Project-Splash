using UnityEngine;
using UnityEngine.UI;

public class TrickManager : MonoBehaviour {

	PlayerMotor pm;
	ScoreManager sc;

    public float manualScore;

	// Use this for initialization
	void Start ()
    {
        pm = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (pm.isManual) {
			manualScore = manualScore + Time.deltaTime;
		}
	}
	public void LandManual()
	{
		sc.AddScore (100f);
	}
}
