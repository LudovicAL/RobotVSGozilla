using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {

	public int hitPoints;
	private float damages;
	private Animator animator;
	private HeatlhBar healthBar;
	private float lastHitTime;
	private float timeBetweenHits;

	// Use this for initialization
	void Start () {
		animator = transform.FindChild ("CharacterRig").gameObject.GetComponent<Animator> ();
		healthBar = GameObject.Find ("Slider").GetComponent<HeatlhBar>();
	}
	
	// Update is called once per frame
	void Update () {
		timeBetweenHits = Mathf.Clamp(10.0f - (5000.0f * healthBar.effectiveness), 1.1f, Mathf.Infinity);
		Debug.Log ((5000.0f * healthBar.effectiveness).ToString() + " / " + timeBetweenHits.ToString ());
		if (hitPoints <= 0) {
			Die ();
		} else if ((Time.time - lastHitTime) > timeBetweenHits) {
			Attack ();
		} else if (healthBar.effectiveness > 0.5) {
			Defend ();
		} else {
			GoIdle ();
		}
	}

	private void SetTriggers(bool defensive, bool attacking, bool hurt, bool dead) {
		animator.SetBool ("Defensive", defensive);
		animator.SetBool ("Attacking", attacking);
		animator.SetBool ("Hurt", hurt);
		animator.SetBool ("Dead", dead);
	}
		
	public void GoIdle() {
		if (animator.GetBool("Dead") != true) {
			SetTriggers (false, false, false, false);
		}
	}

	private void Defend() {
		if (animator.GetBool("Dead") != true) {
			SetTriggers (true, false, false, false);
		}
	}

	private void Attack() {
		if (animator.GetBool("Dead") != true) {
			lastHitTime = Time.time;
			SetTriggers (false, true, false, false);
		}
	}

	public void Hurt() {
		if (animator.GetBool("Dead") != true) {
			SetTriggers (false, false, true, false);
		}
	}

	private void Die() {
		SetTriggers (false, false, false, true);
	}
}
