using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private Transform target;

	private bool followPlayer;

	public float min_Y_Threshold = -2.6f; // how below can player fall before the camera starts following him

	void Awake () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update () {
		Follow();
	}

	void Follow () {

		if (target.position.y < (transform.position.y - min_Y_Threshold)) { // if monkey's position is lower than the current camera's pos Y - threshold
			followPlayer = false;
		}

		if (target.position.y > transform.position.y) { // if monkey is higher than the current camera's pos Y - camera goes above
			followPlayer = true;
		}

		if (followPlayer) {
			Vector3 temp = transform.position;
			temp.y = target.position.y;
			transform.position = temp;
		}

	}
}
