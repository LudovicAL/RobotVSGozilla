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
	private float fadeSpeed;
	private Monster monster;
	private Knight knight;

	// Use this for initialization
	void Start () {
		fadeSpeed = 1.0f;
		knight = GameObject.Find("Knight").GetComponent<Knight>();
		monster = GameObject.Find("Monster").GetComponent<Monster>();
		healthBar = GameObject.Find ("Slider").GetComponent<HeatlhBar>();
		panelEnd = GameObject.Find ("PanelEnd");
		panelEndImage = panelEnd.GetComponent<Image>();
		message = GameObject.Find ("Message").GetComponent<Text>();
		startOver = GameObject.Find ("StartOver").GetComponent<Text>();
		initialColor = panelEndImage.color;
		panelEnd.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!panelEnd.activeSelf) {
			if (healthBar.slider.value <= 0) {	//Defeat
				panelEnd.SetActive(true);
				message.text = "Vous avez été écrasé!";
				StartCoroutine (ShowCountDown());
			} else if (healthBar.slider.value >= 1) {	//Victory
				panelEnd.SetActive(true);
				message.text = "Vous avez vaincu!";
				StartCoroutine (ShowCountDown());
			}
		} else {
			ActivatePanelEnd();
		}
	}

	private void ActivatePanelEnd() {
		if (panelEndImage.color.a < 1.0f) {
			Color tempoColor = panelEndImage.color;
			tempoColor.a = Mathf.Lerp (tempoColor.a, 1.0f, fadeSpeed * Time.deltaTime);
			panelEndImage.color = tempoColor;
		}
	}

	//Shows a countdown before the game actually restarts
	IEnumerator ShowCountDown() {
		for (int i = 10; i > 0; i--) {
			startOver.text = "La partie recommence dans... " + i.ToString();    
			yield return new WaitForSeconds(1.0f);
		}
		healthBar.slider.value = 0.5f;
		knight.animator.SetBool ("Defensive", false);
		knight.animator.SetBool ("Attacking", false);
		knight.animator.SetBool ("Hurt", false);
		knight.animator.SetBool ("Dead", false);
		monster.animator.SetBool ("Attacking", false);
		monster.animator.SetBool ("Hurt", false);
		monster.animator.SetBool ("Dead", false);
		panelEndImage.color = initialColor;
		panelEnd.SetActive (false);
	}
}
