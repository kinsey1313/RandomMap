using UnityEngine;
using System.Collections;

public class SunOrbit : MonoBehaviour {

    public int speed;
	// Use this for initialization
	void Start () {
	
	}
	
	//rotates a gameobject whiche the sun follows
	void Update () {
        transform.Rotate(Vector3.right * speed * Time.deltaTime);
	
	}
}
