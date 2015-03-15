using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float speed = 0.0005f;


	void Update () {

		float newX = transform.position.x + Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		float newZ = transform.position.z + Input.GetAxis ("Vertical") * speed * Time.deltaTime;

		transform.position = new Vector3 (newX, transform.position.y, newZ);


	}
}
