using UnityEngine;
using System.Collections;
using System;

public class Movement : MonoBehaviour {
	public GameObject baseProjectile;
	public Transform gunRight;
	public Transform trans;
	public Inventory2 inventory;
	public Canvas inventoryGUI;
	public float startHeight;
	public float gravity;
	public float stepSize;
	public float turnRate;
	public float jumpForce;
	public float fireCooldown;
	public float projectileSpeed;
	public int mass;

	enum ButtonLabel : int { FIRE, VERTICAL, HORIZONTAL, TURN, JUMP, INVENTORY };
	readonly string[] buttons = { "Fire", "Vertical", "Horizontal", "Turn", "Jump", "Inventory" };
	bool[] buttonsPressed;
	bool canJump;
	bool inventoryOpen;
	float vSpeed;
	float gunCooldown;
	Vector3 speed;

	// Use this for initialization
	void Start() {
		buttonsPressed = new bool[buttons.Length];
		restart();
	}

	private void FixedUpdate() {
		doJumpCalculations();
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
		if(!inventoryOpen) {
			// Sets the axis vector to represent the analogue alignment of a joystick. +/- 1 for discrete keypresses.
			Vector3 axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			// Stops the input from generating a vector with magnitude greater than one, in case analogue sticks' input aren't perfectly circular
			if(axis.magnitude > 1)
				axis.Normalize();
			// Calculates speed based on position after translation minus before translation
			speed = trans.position;
			trans.Translate(axis * stepSize * 0.1f);
			speed = (trans.position - speed) / Time.fixedDeltaTime;
			// Adds in the vSpeed
			speed = new Vector3(speed.x, vSpeed, speed.y);
			// Rotating from mouse movement
			trans.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Turn") * turnRate * 0.0001f);
		}
	}

	void doActions() {
		if(buttonsPressed[(int)ButtonLabel.FIRE]) {
			fire();
		}
		if(buttonsPressed[(int)ButtonLabel.JUMP]) {
			jump();
		}
		if(buttonsPressed[(int)ButtonLabel.INVENTORY]) {
			toggleInventory();
			buttonsPressed[(int)ButtonLabel.INVENTORY] = false;
		}
	}

	void restart() {
		inventoryOpen = false;
		inventoryGUI.enabled = false;
		canJump = true;
		vSpeed = 0.0f;
		gunCooldown = 0.0f;
		for(int i = 0; i < buttonsPressed.Length; i++) {
			buttonsPressed[i] = false;
		}
	}

	void fire() {
		if(!inventoryOpen && gunCooldown <= 0) {
			// Instantiates a new projectile from the "guns" position, also inheriting it's rotation.
			GameObject projectile = (GameObject)Instantiate(baseProjectile, gunRight.position, gunRight.rotation);
			// Sets the projectile velocity as a sum of projectilespeed and the parent's calulated speed.
			projectile.GetComponent<Rigidbody>().velocity = trans.forward * projectileSpeed + speed;
			gunCooldown = fireCooldown;
		}
	}

	void jump() {
		if(!inventoryOpen && canJump) {
			// TODO: Use rigidbody instead
			vSpeed = jumpForce / mass;
			canJump = false;
		}
	}

	private void doJumpCalculations() {
		if(vSpeed != 0 || trans.position.y != startHeight) {
			if(trans.position.y > startHeight) {
				// Reduces speed given the gravity magnitude
				vSpeed -= gravity;
			}
			// Calculates new vertical position
			float y = trans.position.y + vSpeed;
			// Accounts for hitting the ground
			if(y < startHeight) {
				vSpeed = 0;
				y = startHeight;
				canJump = true;
			}
			// Updates the final vertical position
			trans.position = new Vector3(trans.position.x, y, trans.position.z);
		}
	}

	void toggleInventory() {
		inventoryOpen = !inventoryOpen;
		inventoryGUI.enabled = inventoryOpen;
	}

	/**
	 * Updates the cooldown counter for the gun(s).
	 */
	void cooldownTick() {
		if(gunCooldown > 0) {
			gunCooldown -= Time.fixedDeltaTime;
		}
	}
}
