﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatlhBar : MonoBehaviour {

    private List<Machine> machineList;
    public float globalPower { get; private set; }
    public int activeMachines { get; private set; }
	public Slider slider { get; private set; }
	public float effectiveness { get; private set; }
	private GameObject knightArrow;
	private GameObject monsterArrow;

    void Start() {
		knightArrow = GameObject.Find("ImageKnightArrow");
		monsterArrow = GameObject.Find("ImageMonsterArrow");
        GameObject[] bob = GameObject.FindGameObjectsWithTag("Machine");
        machineList = new List<Machine>();
        foreach (GameObject go in bob) {
            machineList.Add(go.GetComponent<Machine>());
        }
        slider = this.GetComponent<Slider>();
		InvokeRepeating ("TimedUpdate", 1.0f, 1.0f);
	}
	
	void Update () {
		GetGlobalPower ();
		ComputeEffectiveness ();
		SetArrows ();
	}

	private void SetArrows() {
		if (effectiveness > 0) {
			knightArrow.SetActive (true);
			monsterArrow.SetActive (false);
		} else {
			knightArrow.SetActive (false);
			monsterArrow.SetActive (true);
		}
	}

	void TimedUpdate() {
		slider.value = Mathf.Clamp(slider.value + effectiveness, 0.0f, 1.0f);
	}

	private void GetGlobalPower() {
		globalPower = 0.0f;
		activeMachines = 0;
		foreach (Machine m in machineList) {     
			globalPower += m.power;
			if (m.power > 0.0f) {
				activeMachines++;
			}
		}
	}

	private void ComputeEffectiveness() {
		int tempoActiveMachines = Mathf.Max(activeMachines, 1);
		effectiveness = ((globalPower / tempoActiveMachines) - 0.5f - (0.1f * machineList.Count) + (0.1f * tempoActiveMachines)) / 1000.0f;
	}
}
