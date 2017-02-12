using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {

	private Animator animator;
	private HeatlhBar healthBar;
	private float lastHitTime;
	private float timeBetweenHits;
	private Monster monster;
	private ParticleSystem particle3;
	private ParticleSystem particle4;

	// Use this for initialization
	void Start () {
		animator = transform.FindChild ("CharacterRig").gameObject.GetComponent<Animator> ();
		healthBar = GameObject.Find ("Slider").GetComponent<HeatlhBar>();
		monster = GameObject.Find("Monster").GetComponent<Monster>();
		particle3 = GameObject.Find("Particle3").GetComponent<ParticleSystem>();
		particle4 = GameObject.Find("Particle4").GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		timeBetweenHits = Mathf.Clamp(10.0f - (5000.0f * healthBar.effectiveness), 1.1f, Mathf.Infinity);
		if (healthBar.slider.value <= 0) {
			Die ();
		} else if ((Time.time - lastHitTime) > timeBetweenHits) {
			Attack ();
		} else if (healthBar.effectiveness > 0.5) {
			Defend ();
		} else {
			GoIdle ();
		}
	}

	public void GoIdle() {
		if (!animator.GetBool("Dead") && animator.GetBool("Defensive")) {
			animator.SetBool ("Defensive", false);
		}
	}

	private void Defend() {
		if (!animator.GetBool("Dead") && !animator.GetBool("Defensive")) {
			animator.SetBool ("Defensive", true);
		}
	}

	private void Attack() {
		if (!animator.GetBool("Dead") && !animator.GetBool("Attacking")) {
			lastHitTime = Time.time;
			animator.SetBool ("Attacking", true);
			Invoke ("HurtOther", 0.3f);
			Invoke ("StopAttacking", 1.0f);
		}
	}

	public void Hurt() {
		if (!animator.GetBool("Dead") && !animator.GetBool("Hurt") && !animator.GetBool("Attacking")) {
			animator.SetBool ("Hurt", true);
			Invoke ("StopHurting", 1.0f);
		}
	}

	private void Die() {
		if (!animator.GetBool("Dead")) {
			animator.SetBool ("Hurt", false);
			animator.SetBool ("Attacking", false);
			animator.SetBool ("Dead", true);
		}
	}

	private void StopAttacking() {
		animator.SetBool ("Attacking", false);
	}

	private void StopHurting() {
		animator.SetBool ("Hurt", false);
	}

	private void HurtOther() {
		monster.Hurt ();
		BurstParticles ();
	}

	private void BurstParticles() {
		particle3.Emit (10);
		particle4.Emit (10);
	}
}
