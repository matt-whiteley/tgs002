var spring = 50.0;
var damper = 5.0;
var drag = 10.0;
var angularDrag = 5.0;
var distance = 0.2;

private var springJoint : SpringJoint;

function Update ()
{
	// Make sure the user pressed the mouse down
	if (Input.GetMouseButtonDown (0)){
		

		var mainCamera = FindCamera();
			
		// We need to actually hit an object
		var hit : RaycastHit;
		if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),  hit, 100))
			return;
		// We need to hit a rigidbody that is not kinematic
		if (!hit.rigidbody || hit.rigidbody.isKinematic)
			return;
		
		if (!springJoint)
		{
			var go = new GameObject("Rigidbody dragger");
			var body : Rigidbody = go.AddComponent.<Rigidbody>() as Rigidbody;
			springJoint = go.AddComponent.<SpringJoint>();
			body.isKinematic = true;
		}
		
		springJoint.transform.position = hit.point;
		
		springJoint.anchor = Vector3.zero;
		
		
		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.maxDistance = distance;
		springJoint.connectedBody = hit.rigidbody;
		StartCoroutine ("DragObject", hit.distance);
	}
	
	if(springJoint && springJoint.connectedBody){
		DragObject();
	}
	
	
}

function DragObject ()
{
	var oldDrag = springJoint.connectedBody.drag;
	var oldAngularDrag = springJoint.connectedBody.angularDrag;
	springJoint.connectedBody.drag = drag;
	springJoint.connectedBody.angularDrag = angularDrag;
	var mainCamera = FindCamera();
	
	var ray = GetWorldPositionOnPlane (Input.mousePosition, 0);
	springJoint.transform.position = ray;//new Vector3(ray.x,0,ray.y); //ray.GetPoint(distance);
		
		
		
	
	if (springJoint.connectedBody && Input.GetMouseButtonUp(0)) 
	{
		springJoint.connectedBody.drag = oldDrag;
		springJoint.connectedBody.angularDrag = oldAngularDrag;
		springJoint.connectedBody = null;
	}
}

function FindCamera ()
{
	if (GetComponent.<Camera>())
		return GetComponent.<Camera>();
	else
		return Camera.main;
}

function GetWorldPositionOnPlane(screenPosition: Vector3, z : float) // replace screenToWorldPoint with one that actually works
{
	var ray = Camera.main.ScreenPointToRay(screenPosition); //ray from camera focal point through view plane into world
	var xy : Plane;
	xy = new Plane(Vector3.up, new Vector3(0,z,0)); // flat plane where we want to get coordinates of the mouse
	var distance : float;
	xy.Raycast(ray, distance); // intersect ray with plane - distance is length along ray of point of intersection
	
	return ray.GetPoint(distance); // return 3D coordinates at 'distance' along ray
}