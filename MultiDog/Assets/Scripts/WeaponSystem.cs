using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeaponSystem : NetworkBehaviour {
	public GameObject leftProjectile;
    public GameObject rightProjectile;
	public Transform gunRight;
    public Transform gunLeft;
	public Text zeroingText;
	public int[] zeroingDistances;

	public float leftBaseCooldown = 0.5f;
    public float rightBaseCooldown = 0.5f;
	public float leftBulletSpeed = 20.0f;
    public float rightBulletSpeed = 20.0f;
	public float leftBulletRotation = 0.0f;
	public float rightBulletRotation = 0.0f;
	public int leftAmmo = -1;
	public int rightAmmo = -1;

	public struct ZeroingState
	{
		public int distance;
		public float angle;
	}

	// Continuous hold buttons
	enum ButtonLabel : int { FIRE1, FIRE2 };
	readonly string[] buttons = { "Fire1", "Fire2" };
	bool[] buttonsPressed;

	const string zeroingString = "Range - ";

	Vector3 moveDirection = Vector3.zero;
	float leftCooldown = 0.0f;
    float rightCooldown = 0.0f;

	//bool leftProjectileHasScript = true;
	//bool rightProjectileHasScript = true;
	bool leftProjectileHasRB = true;
	bool rightProjectileHasRB = true;
	//bool hasPlayerScript = true;
	bool useHealth = true;

	HealthSystem health;
	public ZeroingState[] zeroStates;

	int zeroStateIndex = -1;
	float zeroingAngle = 0.0f;

	private void Awake() {
		try {
			zeroingText = GameObject.Find("ZeroingText").GetComponent<Text>();
		} catch {
			Debug.Log("No zeroing text found");
		}

		if (zeroingDistances != null && zeroingDistances.Length > 0) {
			zeroStates = new ZeroingState[zeroingDistances.Length];
			for(int i = 0; i < zeroStates.Length; i++)
			{
				ZeroingState state = new ZeroingState();
				state.distance = zeroingDistances[i];
				// Temporary manually added a offset of 0.1 compared to the x value of 0.42 this should show a strong effect
				state.angle = state.distance > 0 ? 90 - Mathf.Atan(5 * state.distance / ((0.1f + gunRight.position.x) / transform.localScale.x)) * 180 / Mathf.PI : 0;
				zeroStates[i] = state;
			}
			zeroStateIndex = 0;
		}

		/*if(!(GetComponent<Player>())) {
			Debug.Log("No Player script found, cannot give projectile a owner reference.");
			hasPlayerScript = false;
		}*/

		/*if(!leftProjectile.GetComponent<Projectile>()) {
			Debug.Log("Left gun projectile lacks the Projectile script and cannot inherit owner references.");
			leftProjectileHasScript = false;
		}*/
		if(!leftProjectile.GetComponent<Rigidbody>()) {
			Debug.Log("Left gun projectile lacks a rigidbody and cannot inherit velocity or angular velocity.");
			leftProjectileHasRB = false;
		}
		/*if(!rightProjectile.GetComponent<Projectile>()) {
			Debug.Log("Right gun projectile lacks the Projectile script and cannot inherit owner references.");
			rightProjectileHasScript = false;
		}*/
		if(!rightProjectile.GetComponent<Rigidbody>()) {
			Debug.Log("Right gun projectile lacks a rigidbody and cannot inherit velocity or angular velocity.");
			rightProjectileHasRB = false;
		}
		if(!(health = GetComponent<HealthSystem>())) {
			Debug.Log("No HealthSystem found! Cannot die...");
			useHealth = false;
		}

		// TODO: Check if exists
		/*if(isLocalPlayer) {
			CmdSetID(GetComponent<NetworkIdentity>().playerControllerId);
		}*/

		buttonsPressed = new bool[buttons.Length];
		restart();
	}

	/*[Command]
	public void CmdSetID(short id) {
		playerId = id;
	}*/


	private void FixedUpdate() {
		cooldownTick();
	}

	// Update is called once per frame
	void Update() {
		if(isLocalPlayer) {
			doActions();
		}
	}

	void restart() {
		leftCooldown = 0.0f;
		rightCooldown = 0.0f;
		for(int i = 0; i < buttonsPressed.Length; i++) {
			buttonsPressed[i] = false;
		}
	}

	void checkInput() {
		for(int i = 0; i < buttons.Length; i++) {
			if(Input.GetButtonDown(buttons[i]))
				buttonsPressed[i] = true;
			if(Input.GetButtonUp(buttons[i]))
				buttonsPressed[i] = false;
		}
	}

	void doActions() {
		checkInput();
		if(!useHealth || !health.isDead()) {
			if(buttonsPressed[(int)ButtonLabel.FIRE1])
				CmdFireLeft();
			if(buttonsPressed[(int)ButtonLabel.FIRE2])
				CmdFireRight();
			if (Input.GetButtonDown("Zeroing Up"))
				ZeroingUp();
			if (Input.GetButtonDown("Zeroing Down"))
				ZeroingDown();
		}
	}

	void ZeroingUp()
	{
		if (zeroStateIndex < 0)
			return;
		if (zeroStateIndex < zeroStates.Length - 1)
		{
			zeroStateIndex++;
		}
		if(zeroingText)
			zeroingText.text = zeroingString + zeroStates[zeroStateIndex].distance + "m";
		zeroingAngle = zeroStates[zeroStateIndex].angle;
	}
	void ZeroingDown()
	{
		if (zeroStateIndex < 0)
			return;
		if (zeroStateIndex > 0)
		{
			zeroStateIndex--;
		}
		if (zeroingText)
			zeroingText.text = zeroingString + zeroStates[zeroStateIndex].distance + "m";
		zeroingAngle = zeroStates[zeroStateIndex].angle;
	}

	[Command]
	void CmdFireLeft() {
		if(leftCooldown <= 0) {
			// Return if no ammo, ammo == -1 ignores these if statements
			if(leftAmmo == 0)
				return; // Out of ammo
			else if(leftAmmo > 0)
				leftAmmo--;

			// Sets cooldown and instantiates the projectile
			leftCooldown = leftBaseCooldown;
			GameObject projectile = Instantiate(leftProjectile, gunLeft.position, gunLeft.rotation);
			if(zeroingAngle > 0)
			{
				projectile.transform.Rotate(0, zeroingAngle, 0);
			}

			// Sets the Player script as owner inside the projectile's script for harvesting purposes
/*			if(hasPlayerScript && leftProjectileHasScript)
				projectile.GetComponent<Projectile>().owner = playerId;
*/
			// Sets the projectiles motion variables if it has a Rigidbody component
			if(leftProjectileHasRB) {
				Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
				projectileRB.velocity = projectileRB.transform.forward * leftBulletSpeed + moveDirection;
				projectileRB.angularVelocity = new Vector3(0, leftBulletRotation, 0);
			}
			NetworkServer.Spawn(projectile);
		} // An else statement here to code "gun on cooldown" response
	}

	[Command]
	void CmdFireRight() {
		if(rightCooldown <= 0) {
			// Return if no ammo, ammo == -1 ignores these if statements
			if(rightAmmo == 0)
				return; // Out of ammo
			else if(rightAmmo > 0)
				rightAmmo--;

			// Sets cooldown and instantiates the projectile
			rightCooldown = rightBaseCooldown;
			GameObject projectile = Instantiate(rightProjectile, gunRight.position, gunRight.rotation);
			if (zeroingAngle > 0)
			{
				projectile.transform.Rotate(0, -zeroingAngle, 0);
			}

			// Sets the Player script as owner inside the projectile's script for harvesting purposes
			/*			if(hasPlayerScript && rightProjectileHasScript)
							projectile.GetComponent<Projectile>().owner = playerId;
			*/
			// Sets the projectiles motion variables if it has a Rigidbody component
			if (rightProjectileHasRB) {
				Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
				projectileRB.velocity = projectileRB.transform.forward * rightBulletSpeed + moveDirection;
				projectileRB.angularVelocity = new Vector3(0, rightBulletRotation, 0);
			}
			NetworkServer.Spawn(projectile);
		} // An else statement here to code "gun on cooldown" response
	}

	/**
	 * Updates the cooldown counter for the guns.
	 */
	void cooldownTick() {
		if(leftCooldown > 0) {
			leftCooldown -= Time.fixedDeltaTime;
		}
        if (rightCooldown > 0)
        {
            rightCooldown -= Time.fixedDeltaTime;
        }
    }
}
