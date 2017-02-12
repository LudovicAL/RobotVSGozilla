using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	public float timeBetweenHits;
	private Animator animator;
	private HeatlhBar healthBar;
	private float lastHitTime;
	private Knight knight;
	private ParticleSystem particle1;
	private ParticleSystem particle2;

	// Use this for initialization
	void Start () {
		animator = transform.FindChild ("CharacterRig").gameObject.GetComponent<Animator> ();
		healthBar = GameObject.Find ("Slider").GetComponent<HeatlhBar>();
		knight = GameObject.Find("Knight").GetComponent<Knight>();
		particle1 = GameObject.Find("Particle1").GetComponent<ParticleSystem>();
		particle2 = GameObject.Find("Particle2").GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (healthBar.slider.value >= 1) {
			Die ();
		} else if ((Time.time - lastHitTime) > timeBetweenHits) {
			Attack ();
		}
	}

	private void Attack() {
		if (!animator.GetBool("Dead") && !animator.GetBool("Attacking")) {
			lastHitTime = Time.time;
			animator.SetBool ("Attacking", true);
			Invoke ("StopAttacking", 1.0f);
			Invoke ("HurtOther", 0.3f);
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
		knight.Hurt ();
		BurstParticles ();
	}

	private void BurstParticles() {
		particle1.Emit (10);
		particle2.Emit (10);
	}
}
