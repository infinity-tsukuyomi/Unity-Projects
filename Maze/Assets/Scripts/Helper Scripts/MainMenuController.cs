using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public Animator levelPanelAnim;

	public void PlayGame () {
		levelPanelAnim.Play("SlideIn");
	}
	
	public void Back () {
		levelPanelAnim.Play("SlideOut");
	}
}
