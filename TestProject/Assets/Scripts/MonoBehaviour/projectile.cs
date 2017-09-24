using UnityEngine;

public class projectile : MonoBehaviour {
	public Transform trans;
	public Rigidbody rb;
	public Player owner;
	public float baseSpeed = 2;
	public float lifespan = 10;

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

	private void OnCollisionEnter(Collision collision) {
		Harvestable harvestable = collision.gameObject.GetComponent<Harvestable>();
		if(harvestable && owner) {
			owner.harvest(harvestable);
			Destroy(gameObject);
		}
	}
}
