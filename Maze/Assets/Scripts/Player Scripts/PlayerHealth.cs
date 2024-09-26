using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int health = 100;
	private PlayerScript playerScript;

	private Animator anim;


	void Awake () {
		playerScript = GetComponent<PlayerScript>();
		anim = GetComponent<Animator>();
	}

	void Start () {
		GameplayController.instance.DisplayHealth(health);
	}
	
	public void ApplyDamage (int damageAmount) {
		health -= damageAmount;

		if (health < 0) {
			health = 0;
		}

		GameplayController.instance.DisplayHealth(health); // if health < 0 we don't wanna display the health

		if (health == 0) {
			playerScript.enabled = false;
			anim.Play(MyTags.DEAD_ANIMATION);

			GameplayController.instance.isPlayerAlive = false;

			GameplayController.instance.GameOver();
		}
	}

	void OnTriggerEnter (Collider target) { // when we collide with coin

		if (target.tag == MyTags.COIN_TAG) {
			target.gameObject.SetActive(false); // if we collided with the coin - deactivate it

			GameplayController.instance.CoinCollected(); // increment th coin count on 1
			SoundManager.instance.PlayCoinSound(); // play the coin sound 
		}

	}
}
