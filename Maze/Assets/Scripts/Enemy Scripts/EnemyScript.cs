using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	private GameObject player;
	private Animator anim;
	private Rigidbody myBody;

	private float enemy_Speed = 10f;

	private float enemy_Watch_Threshold = 70f; // distance between enemy and player to start attacking
	private float enemy_Attack_Threshold = 6f;

	public GameObject damagePoint;

	void Awake () {
		player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
		myBody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
		
		if (GameplayController.instance.isPlayerAlive) {
			
			EnemyAI();

		} else {

			if (anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUN_ANIMATION) || anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.ATTACK_ANIMATION)) {
				anim.SetTrigger(MyTags.STOP_TRIGGER);
			}

		}
	}

	void EnemyAI () {
		Vector3 direction = player.transform.position - transform.position; // from players position our own position
		float distance = direction.magnitude; // how long is this vector
		direction.Normalize(); // making vector to keep the direction but his length will be 1.0

		Vector3 velocity = direction * enemy_Speed; // enemy's speed

		if (distance > enemy_Attack_Threshold && distance < enemy_Watch_Threshold) { // if the length between attack threshold and watch threshold we're gonna start chasing player
			myBody.velocity = new Vector3(velocity.x, myBody.velocity.y, velocity.z); // for Y axis we don't wanna move him upwards

			if (anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.ATTACK_ANIMATION)) {
				anim.SetTrigger(MyTags.STOP_TRIGGER);
			}

			anim.SetTrigger(MyTags.RUN_TRIGGER);

			transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z)); // rotate enemy to look towards the player

		} else if (distance < enemy_Attack_Threshold) {
			
			if (anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUN_ANIMATION)) {
				anim.SetTrigger(MyTags.STOP_TRIGGER);
			}

            anim.SetTrigger(MyTags.ATTACK_TRIGGER);

			transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

		} else { // if we ran away from the enemy
			myBody.velocity = new Vector3(0f, 0f, 0f);

			if (anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.ATTACK_ANIMATION) || anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUN_ANIMATION)) {
                anim.SetTrigger(MyTags.STOP_TRIGGER);
            }
		}
	}

	void ActivateDamagePoint () {
		damagePoint.SetActive(true);
	}
	
	void DeactivateDamagePoint () {
		damagePoint.SetActive(false);
	}
}
