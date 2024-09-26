using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

	public int damageAmount = 2;
	public LayerMask playerLayer;
	
	void Update () {
		Collider[] hits = Physics.OverlapSphere(transform.position, 0.1f, playerLayer); // will create a sphere at this position, radius of the sphere, detecting collision

		if (hits.Length > 0) { // we actually dealt the damage

			if (hits[0].gameObject.tag == MyTags.PLAYER_TAG) {
				hits[0].gameObject.GetComponent<PlayerHealth>().ApplyDamage(damageAmount);
			}

		}
	}
}
