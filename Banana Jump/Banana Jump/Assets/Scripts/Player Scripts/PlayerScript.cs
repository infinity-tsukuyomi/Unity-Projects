using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	private Rigidbody2D myBody;

	public float move_Speed = 2f;

	public float normal_Push = 10f;
	public float extra_Push = 14f;

	private bool initial_Push;

	private int push_Count;

	private bool player_Died;

	void Awake () {
		myBody = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		Move();
	}

	void Move () { // player movement

		if (player_Died) {
			return; // all below will be executed and we'll not be able to move
		}

		if (Input.GetAxisRaw("Horizontal") > 0) { // if we press <- or A we'll move to the left
			myBody.velocity = new Vector2(move_Speed, myBody.velocity.y);
		} else if (Input.GetAxisRaw("Horizontal") < 0) {
			myBody.velocity = new Vector2(-move_Speed, myBody.velocity.y);
		}

	}

	void OnTriggerEnter2D (Collider2D target) {

		if (player_Died) {
			return;
		}

		if (target.tag == "ExtraPush") { // we landed on a double banana

			if (!initial_Push) { // if we don't have initial_Push
				initial_Push = true;

				myBody.velocity = new Vector2(myBody.velocity.x, 18f);

				target.gameObject.SetActive(false);

				SoundManager.instance.JumpSoundFX();

				return; // exit from the on trigger enter because of initial push
			} 

		} // initial push


		// OUTSIDE OF THE INITIAL PUSH
		if (target.tag == "NormalPush") {
			myBody.velocity = new Vector2(myBody.velocity.x, normal_Push);

			target.gameObject.SetActive(false);

			push_Count++;

			SoundManager.instance.JumpSoundFX();
		}
		
		if (target.tag == "ExtraPush") {
			myBody.velocity = new Vector2(myBody.velocity.x, extra_Push);

			target.gameObject.SetActive(false);

			push_Count++;

			SoundManager.instance.JumpSoundFX();
		}

		if (push_Count == 2) {
			push_Count = 0;
			PlatformSpawner.instance.SpawnPlatforms(); // spawning new platforms
		}

		if (target.tag == "FallDown" || target.tag == "Bird") {
			player_Died = true;

			SoundManager.instance.GameOverSoundFX();

			GameManager.instance.RestartGame();
		}

	}
}
