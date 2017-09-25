using UnityEngine;

public class RigidMovement : MonoBehaviour {
	public GameObject baseProjectile;
    public GameObject specialProjectile;
	public Transform gunRight;
    public Transform gunLeft;
    public Camera cam;
    public Camera cam2;

	public float gravity = 20.0f;
	public float walkSpeed = 3.0f;
    public float runSpeed = 5.0f;
	public float turnRate = 5.0f;
	public float jumpSpeed = 8.0f;
	public float fireCooldown = 0.2f;
    public float altfireCooldown = 1.0f;
	public float projectileSpeed = 30.0f;
    public float specialProjectileSpeed = 10.0f;
    public float bulletRotation = 0.0f;

    enum ButtonLabel : int { FIRE, VERTICAL, HORIZONTAL, TURN, JUMP, INVENTORY, ALT_FIRE, RUN, LOOK, POV };
	readonly string[] buttons = { "Fire", "Vertical", "Horizontal", "Turn", "Jump", "Inventory", "2nd fire", "Run", "Look", "pov" };
	bool[] buttonsPressed;
    bool tpCamChange = false;
	float gunCooldown;
    float altCooldown;
	Vector3 moveDirection = Vector3.zero;
	Inventory inventory;

	// Use this for initialization
	void Start() {
		if(!(cam = GetComponentInChildren<Camera>())) {
			Debug.Log("Camera not attached, disabling vertical look");
		}
		if(!(inventory = GetComponent<Inventory>())) {
			Debug.Log("Inventory not attached, disabling inventory menu");
		}
		buttonsPressed = new bool[buttons.Length];
		restart();
        cam.gameObject.SetActive(!tpCamChange);
        cam.gameObject.SetActive(tpCamChange);
	}

	private void FixedUpdate() {
		if(!inventory || !inventory.isInventoryOpen())
			turning();
		cooldownTick();
	}

	// Update is called once per frame
	void Update() {
		checkInput();
		if(!inventory || !inventory.isInventoryOpen())
			doActions();
		movement();
	}

	void checkInput() {
		for(int i = 0; i < buttons.Length; i++) {
			if(Input.GetButtonDown(buttons[i]))
				buttonsPressed[i] = true;
			if(Input.GetButtonUp(buttons[i]))
				buttonsPressed[i] = false;
		}
	}

	void movement() {
		CharacterController controller = GetComponent<CharacterController>();
		if(controller.isGrounded && (!inventory || !inventory.isInventoryOpen())) {
			// Sets the axis vector to represent the analogue alignment of a joystick. +/- 1 for discrete keypresses.
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			// Stops the input from generating a vector with magnitude greater than one, in case analogue sticks' input aren't perfectly circular
			if(moveDirection.magnitude > 1)
				moveDirection.Normalize();
			float movementSpeed = buttonsPressed[(int)ButtonLabel.RUN] ? runSpeed : walkSpeed;
			moveDirection = transform.TransformDirection(moveDirection) * movementSpeed;
			if(Input.GetButtonDown("Jump"))
				moveDirection.y = jumpSpeed;
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	void turning() {
		// Rotating from mouse movement
		transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Turn") * turnRate * 0.0001f);
		//GetComponent<Rigidbody>().AddTorque(0, Input.GetAxis("Turn") * turnRate * 0.0001f, 0);
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
        if (Input.GetButtonDown ("pov")) {
            changeCam();
        }
	}

	void restart() {
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
			projectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed + moveDirection;
			projectile.GetComponent<projectile>().owner = GetComponent<Player>();
			gunCooldown = fireCooldown;
		}
	}

    void altfire() {
        if (altCooldown <= 0)
        {
            // Instantiates a new projectile from the "guns" position, also inheriting it's rotation.
            GameObject projectile = (GameObject)Instantiate(specialProjectile, gunLeft.position, gunLeft.rotation);
            // Sets the projectile velocity as a sum of projectilespeed and the parent's calulated speed.
            projectile.GetComponent<Rigidbody>().velocity = transform.forward * specialProjectileSpeed + moveDirection;
            projectile.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, bulletRotation, 0);
			projectile.GetComponent<projectile>().owner = GetComponent<Player>();
			altCooldown = altfireCooldown;
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
        tpCamChange = !tpCamChange;
        cam.gameObject.SetActive(!tpCamChange);
        cam2.gameObject.SetActive(tpCamChange);
           
    }
}
