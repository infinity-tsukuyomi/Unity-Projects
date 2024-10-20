using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {

	public float speed = 5f;
	private Rigidbody myBody;
	private bool onWood;

	private KnifeSpawner knifeSpawner;

	void Awake () {
		myBody = GetComponent<Rigidbody>();

		knifeSpawner = GameObject.Find("Knife Spawner").GetComponent<KnifeSpawner>();
	}
	
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space) && !onWood) {
			myBody.velocity = new Vector3(0f, speed, 0f);
		}

	}

	void OnTriggerEnter (Collider target) {
		if (target.tag == "Wood") {
			gameObject.transform.SetParent(target.transform); // we get wood and knife in the same parent(to rotate them together)
			myBody.velocity = Vector3.zero; // stop the knife
			onWood = true;
			myBody.detectCollisions = false; // not detect collision anymore

			knifeSpawner.SpawnKnife();
			knifeSpawner.IncrementScore();
		}

		if (target.tag == "Knife") {
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}
	}
}
