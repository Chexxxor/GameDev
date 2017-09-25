using UnityEngine;
using System.Collections;

public class Weekly1 : MonoBehaviour {
    public Canvas canvas;
    //public GUIText winText;
    public Transform trans;
    public int x, z, tx, tz;
	private bool hasChanged;

	// Use this for initialization
	void Start () {
		x = 0;
		z = 0;
		tx = 2; 
		tz = 4;
		hasChanged = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.W)) {
			z++;
			hasChanged = true;
		}
		if (Input.GetKeyDown (KeyCode.S)){
			z--;
			hasChanged = true;
		}
		if (Input.GetKeyDown (KeyCode.D)){
			x++;
			hasChanged = true;
		}
		if (Input.GetKeyDown (KeyCode.A)){
			x--;
			hasChanged = true;
		}
		if (hasChanged) {
            trans.position = new Vector3(x, 0, z);
			//printPos ();
		}
		if (x == tx && z == tz && hasChanged) {
			print ("Found treasure!");
		}
		hasChanged = false;
	}

	void printPos(){
		Debug.Log (x + ", " + z);
	}
}
