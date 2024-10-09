using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

	public static PlatformSpawner instance;

	[SerializeField]
	private GameObject left_Platform, right_Platform;

	private float left_X_Min = -4.4f, left_X_Max = -2.8f, right_X_Min = 4.4f, right_X_Max = 2.8f;
	private float y_Threshold = 2.6f; // difference between the hight of two platforms
	private float last_Y; // variable to spawn platforms

	public int spawn_Count = 8; // when we call the function to spawn platform - we're going to spawn 8 new platforms
	private int platform_Spawned;

	[SerializeField]
	private Transform platform_Parent; // one game object for all spawned platforms 

	[SerializeField]
	private GameObject bird;
	public float bird_Y = 5f;
	private float bird_X_Min = -2.3f, bird_X_Max = 2.3f;

	void Awake () {

		if (instance == null) {
            instance = this;
        }

	}

	void Start () {
		last_Y = transform.position.y; // current Y pos of gameobject
        
		SpawnPlatforms();
	}
	
	public void SpawnPlatforms () {
		Vector2 temp = transform.position;
		GameObject newPlatform = null; // we need to add the parent for this platform

		for (int i = 0; i < spawn_Count; i++) {
			temp.y = last_Y;

			// we have even number (есть ли остаток и делится ли platform_Spawned нацело на 2)
			if ((platform_Spawned % 2) == 0) { // how many times 2 can go in the platform_Spawned
				temp.x = Random.Range(left_X_Min, left_X_Max); // we use left pos for right platforms because we have our display turned around in unity

				newPlatform = Instantiate(right_Platform, temp, Quaternion.identity);
			} else { 
				// if we have odd(нечетное c остатком) number
				temp.x = Random.Range(right_X_Min, right_X_Max); 

				newPlatform = Instantiate(left_Platform, temp, Quaternion.identity);
			}

			newPlatform.transform.parent = platform_Parent; // we're pasting all our platforms in created parent
			// spawning in zig-zag form
			last_Y += y_Threshold; 
			platform_Spawned++;
		}

		if (Random.Range(0, 2) > 0) {
			SpawnBird();
		}

	}

	void SpawnBird () {
		Vector2 temp = transform.position;
		temp.x = Random.Range(bird_X_Min, bird_X_Max);
		temp.y += bird_Y;

		GameObject newBird = Instantiate(bird, temp, Quaternion.identity);
		newBird.transform.parent = platform_Parent; 
	}
}
