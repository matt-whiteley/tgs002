using UnityEngine;
using System.Collections;

public class DragHandler : MonoBehaviour {
	public GameObject draggable = null;
	public Rigidbody tile = null;

	public bool atLeastOneFrame = false;

	private Vector3 currentTarget;
	private float perc = 0;

	void Start(){
		currentTarget = this.transform.position;
	}

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
			//Debug.Log("about to drag");

			atLeastOneFrame = DragObject();
		}

	}

	bool DragObject(){
		Vector3 ray = GetWorldPositionOnPlane (Input.mousePosition, 0);


		Vector3 newTarget = ClampToGrid (ray);

		// lerps the tile to the ocation the pinter is in, unless the target chanegs then reset lerp

		if (currentTarget == newTarget) {
			perc += Time.deltaTime * 5f;
		} else {
			currentTarget = newTarget;
			perc = 0f;
		}

		tile.transform.position = Vector3.Lerp (tile.transform.position, ClampToGrid(ray), perc);

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

	Vector3 ClampToGrid(Vector3 point){
		return new Vector3 (Mathf.Round (point.x), 0.0f, Mathf.Round (point.z));
	}
}
