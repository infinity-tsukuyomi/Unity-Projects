using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour {

	private GameObject[] bgs;
	private float height;
	private float highest_Y_Pos;

	void Awake () {
		bgs = GameObject.FindGameObjectsWithTag("BG");
	}
	
	void Start () {
		height = bgs[0].GetComponent<BoxCollider2D>().bounds.size.y; // getting the size y of the BoxCollider of the first element in bgs

		highest_Y_Pos = bgs[0].transform.position.y;

		for (int i = 1; i < bgs.Length; i++) {
			
			if (bgs[i].transform.position.y > highest_Y_Pos) { // we are not sure that our next element will be next not a random one, that's why we're filtering
				highest_Y_Pos = bgs[i].transform.position.y;
			}

		}

	}

	void OnTriggerEnter2D (Collider2D target) {

		if (target.tag == "BG") {
			// we collided with the highest Y BG
			if (target.transform.position.y >= highest_Y_Pos) {
				Vector3 temp = target.transform.position;

				for (int i = 0; i < bgs.Length; i++) {

					if (!bgs[i].activeInHierarchy) { // element in I index is not active (на нем нет галочки включенного объекта)
						temp.y += height;
						bgs[i].transform.position = temp;
						bgs[i].gameObject.SetActive(true);

						highest_Y_Pos = temp.y;
					}

				}
			}

		}

	}
}
