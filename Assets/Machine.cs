using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour {

	public string buttonName;
	public int objectiveRate;
	private List<float> tapTimes;
	public float tapsPerTenSecond { get; private set; }
	public float power { get; private set; }

	void Start() {
		tapTimes = new List<float>();
		if (objectiveRate == 0) {
			objectiveRate = 1;
		}
	}

	void Update () {
		if (Input.GetButtonDown(buttonName)) {
			tapTimes.Add (Time.time);
		}
		if (tapTimes.Count > 0) {
			if (tapTimes[0] < (Time.time - 2.0f)) {
				tapTimes.RemoveAt(0);
			}
		}
		tapsPerTenSecond = tapTimes.Count;
		power = (tapsPerTenSecond / (float)objectiveRate) * 100;
	}
}
