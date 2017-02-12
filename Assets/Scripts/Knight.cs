using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {

	public int hitPoints;
	private float damages;
	private Animator animator;
	private float lastHitTime;
	private float timeBetweenHits;
	private bool attacking;

	// Use this for initialization
	void Start () {
		animator = transform.FindChild ("CharacterRig").gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (hitPoints <= 0) {
			Die ();
		} else if (attacking && (Time.time - lastHitTime) > timeBetweenHits) {
			Attack ();
		} else if (power < 0.5) {
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
		SetTriggers (false, false, false, false);
	}

	private void Defend() {
		SetTriggers (true, false, false, false);
	}

	private void Attack() {
		SetTriggers (false, true, false, false);
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
