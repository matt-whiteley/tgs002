using UnityEngine;
using System.Collections;

public class DragHandler : MonoBehaviour {
	public GameObject draggable = null;
	public Rigidbody tile = null;

	public bool atLeastOneFrame = false;

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			
			if (!Physics.Raycast (ray, out hit, 100)) {
				return;
			}

			if (!hit.rigidbody || hit.rigidbody.isKinematic) return;

			tile = hit.rigidbody;
		}

		if(tile){
			Debug.Log("about to drag");

			rotateonRightClick();

			atLeastOneFrame = DragObject();
		}

	}

	bool DragObject(){
		Vector3 ray = GetWorldPositionOnPlane (Input.mousePosition, 0);
		tile.transform.position = ray;

		if (Input.GetMouseButtonDown(0) && atLeastOneFrame) {
			tile = null;			
			//atLeastOneFrame = false;
			return false;
		}
		return true;
	}

	Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z){
		Ray ray = Camera.main.ScreenPointToRay(screenPosition); //ray from camera focal point through view plane into world
		Plane xy;
		xy = new Plane(Vector3.up, new Vector3(0,z,0)); // flat plane where we want to get coordinates of the mouse
		float distance;
		xy.Raycast(ray, out distance); // intersect ray with plane - distance is length along ray of point of intersection
		
		return ray.GetPoint(distance); // return 3D coordinates at 'distance' along ray
	}

	void rotateonRightClick () {
		if (Input.GetMouseButtonDown (1)) {
			transform.Rotate (0, 90, 0);
		}
	}


}
