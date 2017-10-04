using UnityEngine;
using UnityEngine.Networking;

public class MultiMovement : NetworkBehaviour {
    public Camera cam;
    public Camera cam2;

	public float gravity = 20.0f;
	public float walkSpeed = 8.0f;
    public float runSpeed = 15.0f;
	public float turnRate = 5.0f;
	public float jumpSpeed = 8.0f;

    enum ButtonLabel : int { RUN };
	readonly string[] buttons = {"Run"};
	bool[] buttonsPressed;
    bool tpCamChange = false;
	Vector3 moveDirection = Vector3.zero;
	Vector3 resetPos = Vector3.zero;

	HealthSystem health;
	bool useHealth = true;

	// Use this for initialization
	void Start() {
		buttonsPressed = new bool[buttons.Length];
		restart();
		if(cam && cam2) {
			cam.gameObject.SetActive(!tpCamChange);
			cam.gameObject.SetActive(tpCamChange);
		}
		resetPos = transform.position;
		Cursor.lockState = CursorLockMode.Locked;
		if(!(health = GetComponent<HealthSystem>())) {
			Debug.Log("No HealthSystem found! Cannot die...");
			useHealth = false;
		}
	}

	private void FixedUpdate() {
		if(!isLocalPlayer) {
			return;
		}
		turning();
	}

	// Update is called once per frame
	void Update() {
		if(!isLocalPlayer) {
			return;
		}
		checkInput();
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

	public Vector3 getMovement() {
		return moveDirection;
	}

	void movement() {
		CharacterController controller = GetComponent<CharacterController>();
		if(controller.isGrounded && (!useHealth || !health.isDead())) {
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
		if(!useHealth || !health.isDead()) {
			// Rotating from mouse movement
			transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Turn") * turnRate * 0.0001f);
			//GetComponent<Rigidbody>().AddTorque(0, Input.GetAxis("Turn") * turnRate * 0.0001f, 0);
			if(cam)
				cam.transform.Rotate(Input.GetAxis("Look") * turnRate * 0.0001f, 0, 0);
		}
	}

	void doActions() {
        if (Input.GetButtonDown("Pov")) {
            changeCam();
        }
		if(Input.GetButtonDown("Reset")) {
			transform.position = resetPos;
			if(health.isDead()) {
				health.Revive();
			}
		}
	}

	void restart() {
		for(int i = 0; i < buttonsPressed.Length; i++) {
			buttonsPressed[i] = false;
		}
	}

    void changeCam()
    {
		if(cam && cam2) {
			tpCamChange = !tpCamChange;
			cam.gameObject.SetActive(!tpCamChange);
			cam2.gameObject.SetActive(tpCamChange);
		}
    }
}
