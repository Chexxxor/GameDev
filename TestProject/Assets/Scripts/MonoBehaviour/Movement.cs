using UnityEngine;

public class Movement : MonoBehaviour {
	public GameObject baseProjectile;
    public GameObject specialProjectile;
	public Transform gunRight;
    public Transform gunLeft;

    
    public Camera cam;
    public Camera cam2;
	//public Inventory2 inventory;
	//public Canvas inventoryGUI;

    Transform trans;

	

	public float startHeight;
	public float gravity;
	public float stepSize;
    public float runSpeed;
	public float turnRate;
	public float jumpForce;
	public float fireCooldown;
    public float altfireCooldown;
	public float projectileSpeed;
    public float specialProjectileSpeed;
    public float bulletRotation;
	public int mass;

    enum ButtonLabel : int { FIRE, VERTICAL, HORIZONTAL, TURN, JUMP, INVENTORY, ALT_FIRE, RUN, LOOK, POV };
	readonly string[] buttons = { "Fire", "Vertical", "Horizontal", "Turn", "Jump", "Inventory", "2nd fire", "Run", "Look", "pov" };
	bool[] buttonsPressed;
	bool canJump;
    bool tpCamActive = false;
	float vSpeed;
	float gunCooldown;
    float altCooldown;
	Vector3 speed;
	Inventory inventory;

	// Use this for initialization
	void Start() {
		if(!(cam = GetComponentInChildren<Camera>())) {
			Debug.Log("Camera not attached, disabling vertical look");
		}
		if(!(inventory = GetComponent<Inventory>())) {
			Debug.Log("Inventory not attached, disabling inventory menu");
		}
		trans = GetComponent<Transform>();
		buttonsPressed = new bool[buttons.Length];
		restart();
        cam.gameObject.SetActive(!tpCamActive);
        cam2.gameObject.SetActive(tpCamActive);
	}

	private void FixedUpdate() {
		doJumpCalculations();
		cooldownTick();
        doFixedActions();
	}

	// Update is called once per frame
	void Update() {
		checkInput();
		if(!inventory || !inventory.isInventoryOpen())
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
		// Sets the axis vector to represent the analogue alignment of a joystick. +/- 1 for discrete keypresses.
		Vector3 axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		// Stops the input from generating a vector with magnitude greater than one, in case analogue sticks' input aren't perfectly circular
		if(axis.magnitude > 1)
			axis.Normalize();
		// Calculates speed based on position after translation minus before translation
		speed = trans.position;
        float movementSpeed = buttonsPressed[(int)ButtonLabel.RUN] ? runSpeed : stepSize;
		trans.Translate(axis * movementSpeed * 0.1f);
		speed = (trans.position - speed) / Time.fixedDeltaTime;
		// Adds in the vSpeed
		speed = new Vector3(speed.x, vSpeed, speed.y);
		// Rotating from mouse movement
		trans.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Turn") * turnRate * 0.0001f);
		if(cam)
            cam.transform.Rotate(Input.GetAxis("Look") * turnRate * 0.0001f, 0, 0);
	}

	void doActions() {
		if(buttonsPressed[(int)ButtonLabel.FIRE]) {
			fire();
		}
        if (buttonsPressed[(int)ButtonLabel.ALT_FIRE])
        {
            altfire();
        }
        if (buttonsPressed[(int)ButtonLabel.JUMP]) {
			jump();
		}
        if (Input.GetButtonDown("pov")) {
            changeCam();
        }
	}

	void restart() {
		canJump = true;
		vSpeed = 0.0f;
		gunCooldown = 0.0f;
        altCooldown = 0.0f;
		for(int i = 0; i < buttonsPressed.Length; i++) {
			buttonsPressed[i] = false;
		}
	}

	void fire() {
		if(gunCooldown <= 0) {
			// Instantiates a new projectile from the "guns" position, also inheriting it's rotation.
			GameObject projectile = (GameObject)Instantiate(baseProjectile, gunRight.position, gunRight.rotation);
			// Sets the projectile velocity as a sum of projectilespeed and the parent's calulated speed.
			projectile.GetComponent<Rigidbody>().velocity = trans.forward * projectileSpeed + speed;
			projectile.GetComponent<Projectile>().owner = GetComponent<Player>();
			gunCooldown = fireCooldown;
		}
	}

    void altfire() {
        if (altCooldown <= 0)
        {
            // Instantiates a new projectile from the "guns" position, also inheriting it's rotation.
            GameObject projectile = (GameObject)Instantiate(specialProjectile, gunLeft.position, gunLeft.rotation);
            // Sets the projectile velocity as a sum of projectilespeed and the parent's calulated speed.
            projectile.GetComponent<Rigidbody>().velocity = trans.forward * specialProjectileSpeed + speed;
            projectile.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, bulletRotation, 0);
            altCooldown = altfireCooldown;
        }
    }

    void jump() {
		if(canJump) {
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

	/**
	 * Updates the cooldown counter for the gun(s).
	 */
	void cooldownTick() {
		if(gunCooldown > 0) {
			gunCooldown -= Time.fixedDeltaTime;
		}
        if (altCooldown > 0)
        {
            altCooldown -= Time.fixedDeltaTime;
        }
    }

    void changeCam()
    {
        tpCamActive = !tpCamActive;
        cam.gameObject.SetActive(!tpCamActive);
        cam2.gameObject.SetActive(tpCamActive);
    }
}
