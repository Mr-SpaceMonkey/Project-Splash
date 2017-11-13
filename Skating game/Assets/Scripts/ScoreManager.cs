using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	float score;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void AddScore(float dscore)
	{
		score = score + dscore;
		Debug.Log (score);
	}
	public void AddTimedScore(float dtscore)
	{

	}
}
