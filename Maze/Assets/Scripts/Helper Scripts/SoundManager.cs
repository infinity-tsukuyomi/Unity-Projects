using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;

	[SerializeField]
	private AudioSource coinSoundSource;

	void Awake () {
		MakeInstance();
	}
	
	void MakeInstance () {
		if (instance == null) {
			instance = this;

		} else if (instance != null) {
			Destroy(gameObject);
		}
	}

	public void PlayCoinSound () {
		coinSoundSource.Play();
	} 
}
