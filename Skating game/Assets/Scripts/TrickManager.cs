using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrickManager : MonoBehaviour {

	PlayerMotor pm;
    public TextMeshProUGUI comboScoreText;

    public float manualScorePerSec;
    public float grindScorePerSec;
    float comboScore;

	// Use this for initialization
	void Start ()
    {
        pm = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (pm.isGrinding)
        {
            comboScore = comboScore + grindScorePerSec * Time.deltaTime;
            comboScoreText.SetText("Combo Score: " + Mathf.Round(comboScore));
        }
		if (pm.isManual) {
			comboScore = comboScore + manualScorePerSec * Time.deltaTime;
            comboScoreText.SetText("Combo Score: " + Mathf.Round(comboScore));
		}
        if (pm.isGrounded)
        {
            LandCombo(comboScore);
        }
	}
	void LandCombo(float combo_Score)
	{
        comboScore = 0;
        comboScoreText.SetText("Combo Score: 0");
	}
}
