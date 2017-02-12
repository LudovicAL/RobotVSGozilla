using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMover : MonoBehaviour {

	private Vector3 initialPosition;
	public float movingSpeed;

	// Use this for initialization
	void Start () {
		initialPosition = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.localPosition;
		newPosition.x -= movingSpeed * Time.fixedDeltaTime;
		transform.localPosition = newPosition;
		if (transform.localPosition.x <= -initialPosition.x) {
			transform.localPosition = initialPosition;
		}
	}
}
