using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour {

	public GameObject bloodFXPrefab;
	private float speed = 1f;

	private Rigidbody myBody;

	private bool isAlive;

	void Start () {
		myBody = GetComponent<Rigidbody> ();

		speed = Random.Range (1f, 5f);

		isAlive = true;
	}

	void Update () {

		if (isAlive) {
			myBody.velocity = new Vector3 (0f, 0f, -speed);
		}

		if (transform.position.y < -10f) {
			gameObject.SetActive (false);
		}
	}

	void Die() {
		isAlive = false;

		myBody.velocity = Vector3.zero; // when we die - we stop walking
		GetComponent<Collider> ().enabled = false; // disabling the collider so we can't detect the collision 
		GetComponentInChildren<Animator> ().Play ("Idle"); // play Idle animation to stop walking animation

		transform.rotation = Quaternion.Euler (90f, 0f, 0f); // rotate them like they fall down the ground
		transform.localScale = new Vector3 (1f, 1f, 0.2f); // change scale on Z-axis
		transform.position = new Vector3 (transform.position.x, 0.2f, transform.position.z); // setting them a bit under ground
	}

	void DeactivateGameObject() {
		gameObject.SetActive (false);
	}

	void OnCollisionEnter(Collision target) {
		if (target.gameObject.tag == "Player" || target.gameObject.tag == "Bullet") {
			Instantiate (bloodFXPrefab, transform.position, Quaternion.identity);

			Invoke ("DeactivateGameObject", 3f);

			GameplayController.instance.IncreaseScore ();

			Die ();

		}
	}

} // class





































