using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour {

	public string buttonName;
	public int objectiveRate;
	public int perHowManySeconds;
	private List<float> tapTimes;
	public float tapsPerXSecond { get; private set; }
	public float power { get; private set; }
	public Image image;
	public Color defaultColor;

	void Start() {
		tapTimes = new List<float>();
		if (objectiveRate == 0) {
			objectiveRate = 1;
		}
		image = transform.FindChild ("Image").gameObject.GetComponent<Image>();
		tapsPerXSecond = 0.0f;
		power = 0.0f;
		defaultColor = image.color;
	}

	void Update () {
		if (Input.GetButtonDown(buttonName)) {
			tapTimes.Add (Time.time);
		}
		ComputeTapRate ();
		defaultColor.a = Mathf.Clamp (power, 0, 1); 
		image.color = defaultColor;
	}

	private void ComputeTapRate() {
		if (tapTimes.Count > 0) {
			if (tapTimes[0] < (Time.time - perHowManySeconds)) {
				tapTimes.RemoveAt(0);
			}
		}
		tapsPerXSecond = tapTimes.Count;
		power = (tapsPerXSecond / (float)objectiveRate);
	}
}
