using UnityEngine;

public class projectile : MonoBehaviour {
	public Player owner;
	public Transform trans;
	public Rigidbody rb;
	public float baseSpeed = 2;
	public float lifespan = 10;
	public Harvestable harvestable;

	private float age;

	// Use this for initialization
	void Start () {
		trans.Translate(0, 0, GetComponent<Collider>().bounds.extents.z);
		//rb.velocity = new Vector3(0, 0, baseSpeed);
	}

	// Update is called at fixed times
	private void FixedUpdate() {
		age += Time.fixedDeltaTime;
		if(age > lifespan)
			Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}

	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.CompareTag("Harvestable")) {
			owner.harvest(collision.gameObject.GetComponent<Harvestable>());
		}
	}
}
