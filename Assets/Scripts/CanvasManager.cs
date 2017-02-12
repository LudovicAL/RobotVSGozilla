using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	private HeatlhBar healthBar;
	private GameObject panelEnd;
	private Image panelEndImage;
	private Text message;
	private Text startOver;
	private Color initialColor;


	// Use this for initialization
	void Start () {
		healthBar = GameObject.Find ("Slider").GetComponent<HeatlhBar>();
		panelEnd = GameObject.Find ("PanelEnd");
		panelEndImage = panelEnd.GetComponent<Image>();
		message = GameObject.Find ("Message").GetComponent<Text>();
		startOver = GameObject.Find ("StartOver").GetComponent<Text>();
		initialColor = panelEndImage.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (healthBar.slider.value <= 0) {	//Defeat
			ActivatePanelEnd();
			message.text = "Vous avez été écrasé!";
		} else if (healthBar.slider.value >= 1) {	//Victory
			ActivatePanelEnd();
			message.text = "Vous avez vaincu!";
		}
	}

	private void ActivatePanelEnd() {

	}
}
