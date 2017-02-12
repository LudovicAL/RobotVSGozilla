using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	private AudioSource violinlegato;
	private AudioSource violinstaccato;
	private AudioSource frenchhorn;
	private AudioSource timpani;
	private List<Machine> machineList;


	// Use this for initialization
	void Start () {
		machineList = new List<Machine> ();
		GameObject[] machineListGO = GameObject.FindGameObjectsWithTag ("Machine");
		foreach (GameObject GO in machineListGO) {
			machineList.Add (GO.GetComponent<Machine> ()); 
		} 
		AudioSource[] audiosourcelist = this.GetComponents<AudioSource> ();
		violinlegato = audiosourcelist [1];
		violinstaccato = audiosourcelist [2];
		frenchhorn = audiosourcelist [3];
		timpani = audiosourcelist [0];
	}
	
	// Update is called once per frame
	void Update () {
		float totalpower = 1.0f;
		float violinlegatovol = Mathf.Clamp (machineList [1].power, 0.0f, 1.0f);
		totalpower += violinlegatovol;
		float violinstaccatovol = Mathf.Clamp (machineList [2].power, 0.0f, 1.0f);
		totalpower += violinstaccatovol;
		float frenchhornvol = Mathf.Clamp (machineList [0].power, 0.0f, 1.0f);
		totalpower += frenchhornvol;
		timpani.volume = Mathf.Clamp (1.0f/totalpower, 0.70f, 1.0f);
		violinlegato.volume = violinlegatovol/totalpower;
		violinstaccato.volume = violinstaccatovol/totalpower;
		frenchhorn.volume = frenchhornvol/totalpower;
	}
}
