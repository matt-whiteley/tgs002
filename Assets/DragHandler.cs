using UnityEngine;
using System.Collections;

public class DragHandler : MonoBehaviour {
	public GameObject draggable = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			if (draggable == null) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				
				if (Physics.Raycast (ray, out hit, 100)) {
					draggable = hit.transform.gameObject;

				}
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			draggable = null;
		}
	}
}
	
