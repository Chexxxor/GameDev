using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReappearingCubes : MonoBehaviour
{
    public GameObject Target;
	public float DisableTime = 0.5f;
	public float EnableTime = 3f;

    private void OnCollisionEnter(Collision collision)
    {
        Invoke("DisablePlatform", DisableTime);
        Invoke("EnablePlatform", EnableTime);
    }

    void DisablePlatform()
    {
        Target.SetActive(false);
		GetComponent<UnityEngine.Rigidbody>().WakeUp();
    }

    void EnablePlatform()
    {
        Target.SetActive(true);
		GetComponent<UnityEngine.Rigidbody>().WakeUp();
    }
}
