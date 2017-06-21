using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	public Transform player;
	public float offsset;

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.position.x + offsset, transform.position.y, transform.position.z );
	}

}
