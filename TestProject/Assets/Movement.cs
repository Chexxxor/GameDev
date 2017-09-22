using UnityEngine;
using System.Collections;
using System;

public class Movement : MonoBehaviour {
	public GameObject baseProjectile;
	public Transform gunRight;
	public Transform trans;
	public float startHeight;
	public float gravity;
	public float stepSize;
	public float turnRate;
	public float jumpForce;
	public float fireCooldown;
	public int mass;

	enum ButtonLabel : int { FIRE, VERTICAL, HORIZONTAL, TURN, JUMP };
	readonly string[] buttons = { "Fire", "Vertical", "Horizontal", "Turn", "Jump" };
	bool[] buttonsPressed;
	bool canJump;
	float vSpeed;
	float gunCooldown;

	// Use this for initialization
	void Start() {
		buttonsPressed = new bool[buttons.Length];
		restart();
	}

	private void FixedUpdate() {
		doCalculations();
		doFixedActions();
		cooldownTick();
	}

	// Update is called once per frame
	void Update() {
		checkInput();
		doActions();
	}

	void checkInput() {
		for(int i = 0; i < buttons.Length; i++) {
			if(Input.GetButtonDown(buttons[i]))
				buttonsPressed[i] = true;
			if(Input.GetButtonUp(buttons[i]))
				buttonsPressed[i] = false;
		}
	}

	void doFixedActions() {
		Vector3 axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if(axis.magnitude > 1)
			axis.Normalize();
		trans.Translate(axis * stepSize * 0.1f);
		trans.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Turn") * turnRate * 0.0001f);
	}

	void doActions() {
		if(buttonsPressed[(int)ButtonLabel.FIRE]) {
			fire();
		}
		if(buttonsPressed[(int)ButtonLabel.JUMP]) {
			jump();
		}
	}

	void restart() {
		canJump = true;
		vSpeed = 0.0f;
		gunCooldown = 0.0f;
		for(int i = 0; i < buttonsPressed.Length; i++) {
			buttonsPressed[i] = false;
		}
	}

	void fire() {
		if(gunCooldown <= 0) {
			GameObject projectile = (GameObject)Instantiate(baseProjectile, gunRight.position, gunRight.rotation);
			gunCooldown = fireCooldown;
		}
	}

	void jump() {
		if(canJump) {
			Debug.Log("Jumping");
			vSpeed = jumpForce / mass;
			canJump = false;
		}
	}

	private void doCalculations() {
		if(vSpeed != 0 || trans.position.y != startHeight) {
			if(trans.position.y > startHeight) {
				vSpeed -= gravity;
			}
			float y = trans.position.y + vSpeed;
			if(y < startHeight) {
				vSpeed = 0;
				y = startHeight;
				canJump = true;
			}
			trans.position = new Vector3(trans.position.x, y, trans.position.z);
		}
	}

	void cooldownTick() {
		if(gunCooldown > 0) {
			gunCooldown -= Time.fixedDeltaTime;
		}
	}
}
