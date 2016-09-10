using UnityEngine;
using System.Collections;

public class HeightMap : MonoBehaviour {

    MeshCollider collider;

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(10, 90, 10);
        collider = new MeshCollider();
        //collider.sharedMesh = this.

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
