using UnityEngine;
using System.Collections;

public class SnapHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			
			if( Physics.Raycast( ray, out hit, 100 ) )
			{
				hit.transform.gameObject.transform.Find ("Model").transform.localPosition = new Vector3(0f, 10.0f, 0f);
				hit.transform.gameObject.GetComponent<Renderer>().enabled = true;
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			//this.GetComponent<Rigidbody>().velocity = Vector3.zero;

			this.transform.Find ("Model").transform.localPosition = Vector3.zero;
			this.transform.position = new Vector3(Mathf.Round(this.transform.position.x), 0.0f, Mathf.Round(this.transform.position.z));
			this.GetComponent<Renderer>().enabled = false;
		}
	}
}
