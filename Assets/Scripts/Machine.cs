using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour {

	public string buttonName;
	public int objectiveRate;
	public int perHowManySeconds;
	private List<float> tapTimes;
	private Text caption;
	public float tapsPerXSecond { get; private set; }
	public float power { get; private set; }

	void Start() {
		tapTimes = new List<float>();
		if (objectiveRate == 0) {
			objectiveRate = 1;
		}
		caption = this.GetComponentInChildren<Text> ();
	}

	void Update () {
		if (Input.GetButtonDown(buttonName)) {
			tapTimes.Add (Time.time);
		}
		if (tapTimes.Count > 0) {
			if (tapTimes[0] < (Time.time - perHowManySeconds)) {
				tapTimes.RemoveAt(0);
			}
		}
		tapsPerXSecond = tapTimes.Count;
		power = (tapsPerXSecond / (float)objectiveRate);
		caption.text = power.ToString("P0");
	}
}
