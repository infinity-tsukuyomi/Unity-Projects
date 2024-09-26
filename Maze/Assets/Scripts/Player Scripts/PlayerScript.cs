using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	private Rigidbody myBody;

	private Animator anim;
	private bool isPlayerMoving;

	private float playerSpeed = 0.5f;
	private float rotationSpeed = 4f;

	private float jumpForce = 3f;
	private bool canJump;

	private float moveHorizontal, moveVertical;

	private float rotY = 0f;

	public Transform groundCheck;
	public LayerMask groundLayer;

	public GameObject damagePoint;

	void Awake () {
		myBody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}

	void Start () {
		rotY = transform.localRotation.eulerAngles.y;
	}

	void Update () {
		PlayerMoveKeyboard();
		AnimatePlayer();
		Attack();
		IsOnGround();
		Jump();
	}
	
	void FixedUpdate () {
		MoveAndRotate();
	}

	void PlayerMoveKeyboard () { // will get the input when player presses button on keyboard
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			moveHorizontal = -1; // moving to the left side
		}

		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) {
			moveHorizontal = 0; // when we realize the key - we don't move
		}

		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			moveHorizontal = 1; // moving to the right side
		}

		if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) {
			moveHorizontal = 0;
		}

		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			moveVertical = 1;
		}

		if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) {
			moveVertical = 0;
		}
	}

	void MoveAndRotate () { // move the player according to pressed button
		if (moveVertical != 0) { // we are move
			myBody.MovePosition (transform.position + transform.forward * (moveVertical * playerSpeed)); // from the current position + in forward direction * (with this speed)
		}

		rotY += moveHorizontal * rotationSpeed;
		myBody.rotation = Quaternion.Euler (0f, rotY, 0f);
	}

	void AnimatePlayer () {
		if (moveVertical != 0) { // if we're moving vertical

			if (!isPlayerMoving) { // if player is not moving

				if (!anim.GetCurrentAnimatorStateInfo(0).IsName (MyTags.RUN_ANIMATION)) { // Base Layer in Animator -> accessing animation which is not Run
					isPlayerMoving = true; // we're moving
					anim.SetTrigger (MyTags.RUN_TRIGGER); // setting the trigger to Run animation
				}

			}

		} else {

			if (isPlayerMoving) {

				if (anim.GetCurrentAnimatorStateInfo(0).IsName (MyTags.RUN_ANIMATION)) {
					isPlayerMoving = false;
                    anim.SetTrigger (MyTags.STOP_TRIGGER);
				} 

			}
			
		}
	}

	void Attack () {
		if (Input.GetKeyDown (KeyCode.K)) { // if the button K is pressed

			if (!anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.ATTACK_ANIMATION) || !anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUN_ATTACK_ANIMATION)) {
				// if the animation Attack is not running or RunAttack animation is not running
			    anim.SetTrigger (MyTags.ATTACK_TRIGGER); // set the trigger to attack
			}

		}
	}

	void IsOnGround () {
		canJump = Physics.Raycast (groundCheck.position, Vector3.down, 0.1f, groundLayer); // draw the line from position -> in this direction -> length of line -> if smth is on the layer will be collision
	}

	void Jump () {
		if (Input.GetKeyDown (KeyCode.Space)) {

			if (canJump) {
				canJump = false; // if we jumped - turn it off to prevent double jumping

				myBody.MovePosition (transform.position + transform.up * (jumpForce * playerSpeed)); // move the body from the current pos -> transform up -> with this speed

				anim.SetTrigger (MyTags.JUMP_TRIGGER); 
			}

		}
	}

	void ActivateDamagePoint () {
		damagePoint.SetActive(true);
	}
	
	void DeactivateDamagePoint () {
		damagePoint.SetActive(false);
	}
}
