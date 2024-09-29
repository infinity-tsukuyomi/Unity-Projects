using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public float speed = 5f;
	public float rotate_Speed = 50f;

	public bool canShoot;
	public bool canRotate;
	public bool canMove = true;

	public float bound_X = -11f; // if enemies go from the bound - we wanna destroy them

	public Transform attack_Point;
	public GameObject bulletPrefab;

	private Animator anim;
	private AudioSource explosionSound;

	void Awake () {
		anim = GetComponent<Animator>();
		explosionSound = GetComponent<AudioSource>();
	}

	void Start () {

		if (canRotate) {

			if (Random.Range(0, 2) > 0) { // only if we have 1
				rotate_Speed = Random.Range(rotate_Speed, rotate_Speed + 20f); // sometimes we're rotating faster/slower 
				rotate_Speed *= -1f; // sometimes rotating forwards/backwards
			} else {
				rotate_Speed = Random.Range(rotate_Speed, rotate_Speed + 20f);
			}

		}

		if (canShoot) {
			Invoke("StartShooting", Random.Range(1f, 3f));
		}

	}
	
	void Update () {
		Move();
		RotateEnemy();
	}

	void Move () {

		if (canMove) {
			Vector3 temp = transform.position;
			temp.x -= speed * Time.deltaTime; // enemies move only to the left 
			transform.position = temp;

			if (temp.x < bound_X) {
				gameObject.SetActive(false); // deactivating gameObject after crossing the -11f
			}

		}

	}

	void RotateEnemy () {

		if (canRotate) {
			transform.Rotate(new Vector3(0f, 0f, rotate_Speed * Time.deltaTime), Space.World); // Space.World is where do we wanna rotate asteroids (unity world is everything in scene)
		}

	}

	void StartShooting () {
		GameObject bullet = Instantiate(bulletPrefab, attack_Point.position, Quaternion.identity); // obj we spawn, at this position, with this rotation Quaternion.identity = (0, 0, 0)
		bullet.GetComponent<BulletScript>().is_EnemyBullet = true; 

		if (canShoot) {
			Invoke("StartShooting", Random.Range(1f, 3f));
		}

	}

	void TurnOffGameObject () {
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D (Collider2D target) {

		if (target.tag == "Bullet") {
			canMove = false;

			if (canShoot) {
				canShoot = false;
				CancelInvoke("StartShooting");
			}

		}

		Invoke("TurnOffGameObject", 3f);

		explosionSound.Play();
		anim.Play("Destroy");
	}
}
