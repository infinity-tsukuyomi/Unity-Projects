using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour {

	public GameObject player, playButton;

	public Animator doorsAnim;

	void DeactivateGameObjects() {
		player.SetActive(false);
        playButton.SetActive(false);
	}
	
	void ActivateGameObjects() {
		player.SetActive(true);
        playButton.SetActive(true);
	}

	void OnTriggerEnter(Collider target) {
		if (target.tag == MyTags.PLAYER_TAG) {
			doorsAnim.Play("DoorOpen");
		}
	}
	
	void OnTriggerExit(Collider target) {
		if (target.tag == MyTags.PLAYER_TAG) {
			doorsAnim.Play("DoorClose");
		}
	}
}
